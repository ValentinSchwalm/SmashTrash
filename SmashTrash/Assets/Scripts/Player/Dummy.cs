using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IHealthSystem
{
    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            print("I died lol");
        }
    }

    [SerializeField] int health;
}
