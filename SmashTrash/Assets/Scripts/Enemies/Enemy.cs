using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IHealthSystem
{
    protected Image healthBarUI;
    [SerializeField] protected int damage;

    public int currentHealthPoints()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public int maxHealthPoints()
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveDamage(int damage)
    {
        throw new System.NotImplementedException();
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
