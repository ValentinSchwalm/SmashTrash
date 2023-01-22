using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IHealthSystem
{
    [SerializeField] protected Image healthBarUI;
    [SerializeField] protected int damage;
    protected int currentHealth;
    [SerializeField] protected int maxHealth;

    public void Die()
    {
        Destroy(gameObject);
    }

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        healthBarUI.fillAmount = ((float)currentHealth / (float)maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
