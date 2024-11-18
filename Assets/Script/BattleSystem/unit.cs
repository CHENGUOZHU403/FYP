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
    private bool isDead = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator Move(Transform unitTransform, Vector3 targetPosition, float attackRange)
    {
        float duration = 1f; // Duration of the movement
        float elapsed = 0f;

        Vector3 startingPosition = unitTransform.position;
        Vector3 stoppingPosition = new Vector3(targetPosition.x - attackRange, targetPosition.y, targetPosition.z);

        animator.SetBool("isMove", true);

        while (elapsed < duration)
        {
            unitTransform.position = Vector3.Lerp(startingPosition, stoppingPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the unit ends exactly at the target position
        unitTransform.position = stoppingPosition;
        animator.SetBool("isMove", false);
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void Attack()
    {
        animator.SetTrigger("Attack1");
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0) // determine character is dead;
            isDead = true;
        return isDead;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
            currentHP = maxHP;
    }
}
