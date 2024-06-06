using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float damage;
    GameObject Player = default;
    Rigidbody2D rb;
    [SerializeField] float knockbackForce, knockbackStopTime;
    [SerializeField] public bool beingKnockedBack = false;
    public void Start()
    {
        Player = gameObject.transform.parent.gameObject;
        rb = Player.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if(beingKnockedBack)
        {
            Invoke("stopKnockback", knockbackStopTime);
        }
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
            Knockback(knockbackForce);
        }
    }

    void Knockback(float kF)
    {
        beingKnockedBack = true;
        if(gameObject.transform.rotation == Quaternion.Euler(0f,0f,0f)) 
        {
            rb.velocity = new Vector2(-kF, rb.velocity.y);
        }
        else if(gameObject.transform.rotation == Quaternion.Euler(0f, 0f, 180f)) 
        {
            rb.velocity = new Vector2(kF, rb.velocity.y);
        }
        else if(gameObject.transform.rotation == Quaternion.Euler(0f, 0f, 270f)) 
        {
            rb.velocity = new Vector2(rb.velocity.x, kF*5);
        }
    }

    void stopKnockback()
    {
        beingKnockedBack = false;
    }
}
