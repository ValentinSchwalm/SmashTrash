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
    [SerializeField] private int enemyScore;
    public SaveLoadHighScore saveLoadHighScore;

    public void Die()
    {
        if (saveLoadHighScore != null)
        {
            saveLoadHighScore.currentScore += enemyScore;
        }
        else
        {

        }

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

    protected virtual void Start() { }
}
