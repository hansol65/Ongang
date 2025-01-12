using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Unit Stat")]
    public string unitName;
    public int maxHP;
    public int currentHP;
    public int attackPower;

    public virtual void Initialize(string name, int health, int attack)
    {
        unitName = name;
        maxHP = health;
        currentHP = maxHP;
        attackPower = attack;
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP); // 체력이 0 이하로 내려가지 않게 제한
    }

    public virtual void Attack(Unit target)
    {
        Debug.Log($"{unitName} attacks {target.unitName} for {attackPower} damage!");
        target.TakeDamage(attackPower);
    }

}
