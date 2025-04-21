using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;

public enum UnitState
{
    Idle,
    Searching,
    Fighting,
    Dead
}

public class AttackAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackCooldown = 1f;
    private Rigidbody2D rigidBody;
    private Transform target;
    public bool isColliding = false;
    private float lastAttackTime;
    private UnitState currentState = UnitState.Searching;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        FindEnemy();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case UnitState.Idle:
                break;
            case UnitState.Searching:
                if (target == null)
                {
                    FindEnemy();
                }

                if (target != null && !isColliding)
                {
                    Move(target.position);
                }

                if (isColliding)
                {
                    currentState = UnitState.Fighting;
                }
                break;

            case UnitState.Fighting:
                if (isColliding && target != null)
                {
                    Attack();
                } else
                {
                    currentState = UnitState.Searching;
                }
                break;

            case UnitState.Dead:
                break;
        }
    }

    private void Move(Vector2 targetPosition)
    {
        float direction = targetPosition.x - transform.position.x;

        direction = direction > 0 ? 1 : -1;

        rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);

        GetComponent<SpriteRenderer>().flipX = direction < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log($"{gameObject.name}: {collision.gameObject.name}와 충돌.");
            isColliding = true;
            rigidBody.velocity = Vector2.zero;
            target = collision.transform;

            currentState = UnitState.Fighting;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.transform == target)
        {
            Debug.Log($"{gameObject.name} 가 {collision.gameObject.name} 와 충돌했습니다.");
            isColliding = false;
            target = null;
        }
    }

    private void FindEnemy()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, 1 << enemyLayer);

        if (enemies.Length > 0)
        {
            target = enemies[0].transform;
            Debug.Log($"{gameObject}: {target.name} 발견");
        }
        else
        {
            target = null;
            Debug.LogWarning("Enemy 를 찾을 수 없습니다.");
            currentState = UnitState.Idle;
        }
    }

    private void Attack()
    {
        if (target == null)
        {
            Debug.LogWarning("Enemy 를 찾을 수 없습니다.");
            currentState = UnitState.Searching;
            return;
        }

        // ��Ÿ�� üũ
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            Unit targetUnit = target.GetComponent<Unit>();
            if (targetUnit != null)
            {
                targetUnit.takeDamage(GetComponent<Unit>().attackPower);

                // test
                Unit unit = GetComponent<Unit>();

                unit.stat.Exp += 1;
                Debug.Log($"Exp : {unit.stat.Exp}");

                string jsonData = JsonUtility.ToJson(unit.stat);
                string path = Path.Combine(Application.dataPath, "PlayerData.json");
                File.WriteAllText(path, jsonData);
            }
        }
    }
}
