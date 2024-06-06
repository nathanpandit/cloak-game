using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] bool isAttacking, isHoldingUp, isHoldingDown, canAttack;
    [SerializeField] float attackTime, attackCooldown;
    [SerializeField] GameObject attackArea = default;
    SpriteRenderer sr;
    float timer, cooldownTimer;
    void Start()
    {
        canAttack = true;
        attackArea = transform.GetChild(0).gameObject;
        sr = GetComponent<SpriteRenderer>();
        cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (isHoldingUp)
            {
                attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            else if (isHoldingDown)
            {
                attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
            else if (sr.flipX)
            {
                attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else
            {
                attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            isHoldingUp = true;
        } 
        else
        {
            isHoldingUp = false;
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            isHoldingDown = true;
        }
        else
        {
            isHoldingDown = false;
        }

        if(Input.GetKeyDown(KeyCode.X) && canAttack)
        {
            Attack();
        }

        if(!canAttack)
        {
            if (cooldownTimer < attackCooldown)
            {
                cooldownTimer += Time.deltaTime;
            }
            else
            {
                cooldownTimer = 0;
                canAttack = true;
            }
        }

        if(isAttacking)
        {
            timer += Time.deltaTime;
            if (timer >= attackTime)
            {
                timer = 0;
                isAttacking = false;
                attackArea.SetActive(isAttacking);
            }
        }
    }

    void Attack()
    {
        isAttacking = true;
        attackArea.SetActive(isAttacking);
        canAttack = false;
    }
}
