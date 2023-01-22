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
    [SerializeField] protected GameObject DroppedTrash;
    [SerializeField] protected GameObject DroppedGold;
    [SerializeField] protected int AmountOfTrashDropped;

    public void Die()
    {
        Destroy(gameObject);
        Instantiate(DroppedGold, new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
        for (int i = 0; i < AmountOfTrashDropped; i++)
        {
            Instantiate(DroppedTrash, new Vector3(this.transform.position.x + Random.Range(-1f, 1f), this.transform.position.y + i, this.transform.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
        }
        
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
