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
    public float moveSpeed = 2f; // �̵� �ӵ�
    public float attackCooldown = 1f; // ���� ��Ÿ��
    private Rigidbody2D rigidBody;
    private Transform target; // Ÿ�� (Enemy)
    public bool isColliding = false; // �浹 ���� Ȯ��
    private float lastAttackTime; // ������ ���� �ð�
    private UnitState currentState = UnitState.Searching; // ������ ���� ���� (�ʱⰪ: Ž��)

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // Enemy ���̾ ã�� Ÿ�� ����
        FindEnemy();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case UnitState.Idle:
                // �ƹ��͵� ���� �ʰ� ���
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
                // �ƹ��͵� ���� ���� (���� ��� �ִϸ��̼��̳� ���� ó�� ����)
                break;
        }
    }

    private void Move(Vector2 targetPosition)
    {
        // �ܼ��� x ���⸸ ����Ͽ� �̵�
        float direction = targetPosition.x - transform.position.x;

        // ���⿡ ���� �̵� �ӵ� ���� (Normalize ������� ����)
        direction = direction > 0 ? 1 : -1; // ������: 1, ����: -1

        rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);

        // ��������Ʈ ���� ����
        GetComponent<SpriteRenderer>().flipX = direction < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log($"{gameObject.name}�� {collision.gameObject.name}�� �浹�߽��ϴ�.");
            isColliding = true; // �浹 ���� ����
            rigidBody.velocity = Vector2.zero; // �̵� ����
            target = collision.transform; // �浹�� Enemy�� Ÿ������ ����

            currentState = UnitState.Fighting;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.transform == target)
        {
            Debug.Log($"{gameObject.name}�� {collision.gameObject.name}�� �浹�� �������ϴ�.");
            isColliding = false; // �浹 ���� ����
            target = null; // Ÿ�� �ʱ�ȭ
        }
    }

    private void FindEnemy()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, 1 << enemyLayer);

        if (enemies.Length > 0)
        {
            // ���� ����� ���� Ÿ������ ����
            target = enemies[0].transform;
            Debug.Log($"Ÿ�� ����: {target.name}");
        }
        else
        {
            target = null; // Ÿ�� ����
            Debug.LogWarning("Enemy�� ã�� �� �����ϴ�!");
            currentState = UnitState.Idle; // ��� ����
        }
    }

    private void Attack()
    {
        if (target == null)
        {
            Debug.LogWarning("Ÿ���� �����ϴ�. ������ ����ϴ�.");
            currentState = UnitState.Searching;
            return; // Ÿ���� ������ ���� �ߴ�
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
