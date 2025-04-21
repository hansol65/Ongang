using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rigidBody;
    private Transform target;
    public bool isColliding = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        FindEnemy();
    }

    private void FixedUpdate()
    {
        if (!isColliding && target != null)
        {
            Move(target.position);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log($"{gameObject.name}: {collision.gameObject.name} 와 충돌.");
            isColliding = true;
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void FindEnemy()
    {
        int enemyLayer = LayerMask.NameToLayer("Player");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, 1 << enemyLayer);

        if (enemies.Length > 0)
        {
            target = enemies[0].transform;
            Debug.Log($"{gameObject.name}: {target.name} 발견");
        }
        else
        {
            Debug.LogWarning("플레이어 유닛이 없음!");
        }
    }
}
