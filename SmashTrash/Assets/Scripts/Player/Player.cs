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

    public int Currency 
    { 
        get { return this.currency; }
        set { this.currency = value; }
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

    /// <summary>
    /// The players healthpoints will decrease by the amount of damage
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(int damage)
    {
        this.healthpoints -= damage;

        if (this.healthpoints <= 0)
        {
            this.Die();
        }
    }

    /// <summary>
    /// If the player receives damage and their healthpoints fall below 1 they die
    /// </summary>
    public void Die()
    {
        print("Player died!");
        this.healthpoints = this.maxHealthpoints;
    }

    /// <summary>
    /// Increases the players healthpoints by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseHealthpoints(int amount)
    {
        this.maxHealthpoints += amount;
        this.healthpoints = this.maxHealthpoints;
    }

    /// <summary>
    /// Increases the ammunition of the current weapon by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseWeaponAmmunition(int amount)
    {
        this.currentWeapon.MaxAmmunition += amount;
    }

    /// <summary>
    /// Increases the damage of the current weapon by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseWeaponDamage(int amount)
    {
        this.currentWeapon.Damage += amount;
    }

    /// <summary>
    /// Increases the shooting force (range) of the current weapon by the given amount
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseWeaponRange(float amount)
    {
        this.currentWeapon.ShootingForce += amount;
    }
}
