using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int attackPower;
    public float attackSpeed;

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
    private void Die()
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