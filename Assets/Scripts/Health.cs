using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float healthPoints, maxHealth;
    [SerializeField] float regenerationTime, regenerationAmount;
    float timer;
    void Start()
    {
        healthPoints = maxHealth;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints < maxHealth) {
            if(timer < regenerationTime) {
                timer += Time.deltaTime;
            }
            else {
                timer = 0;
                if(healthPoints + regenerationAmount > maxHealth) {
                    healthPoints = maxHealth;
                }
                else {
                    healthPoints += regenerationAmount;
                }
            }
        }
        else {

            timer = 0;
        }

        if(healthPoints <= 0)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        healthPoints -= damage;
    }
}
