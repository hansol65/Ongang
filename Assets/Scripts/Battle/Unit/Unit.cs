using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP = 10;
    public int attackPower = 1;
    public float attackSpeed = 1;

    public Stat stat = new Stat { Exp = 0 };

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Rigidbody2D rigid;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

    }

    public void takeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
        Debug.Log($"{gameObject.name} 체력: {currentHP}");
    }
    public void Die()
    {
        Debug.Log($"{gameObject.name}이(가) 사망했습니다.");

        // Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // Sprite Flip Y
        spriteRenderer.flipY = true;
        // Collider Disable
        boxCollider.enabled = false;
        // Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        // Destroy
        Invoke("DeActive", 0.5f);
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }

}