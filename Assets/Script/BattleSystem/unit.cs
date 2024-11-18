using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    public float timeRemaining;
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public float attackRange;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack1");
    }
    
    public void setWalkingBool(bool isWalking)
    {
        animator.SetBool("isMove", isWalking);
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if(currentHP <= 0) // determine character is dead;
            return true; 
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
            currentHP = maxHP;
    }
}
