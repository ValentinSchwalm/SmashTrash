using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHealthSystem
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private int currency;
    [SerializeField] private int healthpoints;
    private int maxHealthpoints;

    private void Start()
    {
        this.maxHealthpoints = this.healthpoints;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            this.PrimaryFire();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            this.SecondaryFire();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            this.currentWeapon.OnSuckStop();
        }
    }

    public void PrimaryFire()
    {
        this.currentWeapon.Shoot();
    }

    public void SecondaryFire()
    {
        this.currentWeapon.Suck();
    }

    public void Interact()
    {

    }

    public void ReceiveDamage(int damage)
    {
        this.healthpoints -= damage;

        if (this.healthpoints <= 0)
        {
            this.Die();
        }
    }

    public void Die()
    {
        print("Player died!");
        this.healthpoints = this.maxHealthpoints;
    }

    public int maxHealthPoints()
    {
        throw new System.NotImplementedException();
    }

    public int currentHealthPoints()
    {
        throw new System.NotImplementedException();
    }
}
