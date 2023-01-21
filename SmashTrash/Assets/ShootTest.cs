using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private Transform destination;
    [SerializeField] private float gravity;
    [SerializeField] private float maxThrowforce;

    [SerializeField] private Projectile projectile;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 displacement = new Vector3(this.destination.position.x, this.origin.position.y, this.destination.position.z) - this.origin.position;

        float deltaY = this.destination.position.y - this.origin.position.y;
        float deltaXZ = displacement.magnitude;

        float throwStrength = Mathf.Clamp(Mathf.Sqrt(this.gravity * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaXZ, 2)))), 0.01f, this.maxThrowforce);
        float angle = Mathf.PI / 2f - (0.5f * (Mathf.PI / 2 - (deltaY / deltaXZ)));

        Vector3 initialVelocity = Mathf.Cos(angle) * throwStrength * displacement.normalized + Mathf.Sin(angle) * throwStrength * Vector3.up;

        Projectile newProjectile = Instantiate(this.projectile, this.origin.position, this.origin.rotation);
        newProjectile.InitiateProjectile(1, initialVelocity);
    }
}
