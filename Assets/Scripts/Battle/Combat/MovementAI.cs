using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : MonoBehaviour
{
    public Transform Target;    // 이동 대상
    public float moveSpeed = 2f;    // 이동 속도
    public float stopDistance = 1.5f;   // 멈출 거리
    private int defaultDirection;  // 디폴트 방향(-1: 좌, 0: 가만히, 1: 우)
    private float directionChangeCooldown;  // 방향 변경 쿨타임
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody; // Rigidbody2D 추가

    public LayerMask groundLayer; // 바닥 체크를 위한 레이어

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
        ChangeDirection();
    }

    private void Update()
    {
        // 타겟이 존재하지 않거나 비활성화된 경우 디폴트 행동 수행
        if (Target == null || !Target.gameObject.activeInHierarchy)
        {
            Target = null; // 비활성화된 타겟을 초기화
            DefaultMovement();
        }
        else
        {
            MoveTarget();
        }
    }

    private void MoveTarget()
    {
        // Target과의 거리 계산
        float distance = Vector2.Distance(transform.position, Target.position);

        // 멈출 거리 이상일 경우 Target을 따라감
        if (distance > stopDistance)
        {
            Vector2 direction = (Target.position - transform.position).normalized;

            // 스프라이트 방향 전환
            if (direction.x < 0)
                spriteRenderer.flipX = true; // 왼쪽으로 이동
            else if (direction.x > 0)
                spriteRenderer.flipX = false; // 오른쪽으로 이동

            // Raycast로 바닥 체크
            if (IsGrounded(direction.x))
            {
                rigidBody.velocity = new Vector2(direction.x * moveSpeed, rigidBody.velocity.y); // 속도 설정
            }
            else
            {
                ChangeDirection(); // 바닥이 없으면 방향 전환
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y); // 정지
        }
    }

    private void DefaultMovement()
    {
        // Raycast로 바닥 체크
        if (IsGrounded(defaultDirection))
        {
            rigidBody.velocity = new Vector2(defaultDirection * moveSpeed, rigidBody.velocity.y); // 속도 설정

            // 스프라이트 방향 전환
            if (defaultDirection < 0)
                spriteRenderer.flipX = true; // 왼쪽으로 이동
            else if (defaultDirection > 0)
                spriteRenderer.flipX = false; // 오른쪽으로 이동
        }
        else
        {
            ChangeDirection(); // 바닥이 없으면 방향 전환
        }

        // 쿨타임 지나면 새로운 랜덤 방향 설정
        directionChangeCooldown -= Time.deltaTime;
        if (directionChangeCooldown <= 0)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        defaultDirection = Random.Range(-1, 2);    // 랜덤으로 -1, 0, 1 방향 선택
        directionChangeCooldown = Random.Range(2f, 5f); // 대기 시간 2에서 5초사이
    }

    private bool IsGrounded(float direction)
    {
        // Raycast로 바닥 체크
        Vector2 origin = new Vector2(transform.position.x + direction * 0.5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);
        Debug.DrawRay(origin, Vector2.down * 1f, Color.red); // Raycast 디버그용
        return hit.collider != null;
    }
}