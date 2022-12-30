using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private int currency;

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
}
