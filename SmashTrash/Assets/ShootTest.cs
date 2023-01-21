using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTest : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _destination;
    [SerializeField] private float _maxThrowforce;

    [SerializeField] private Projectile projectile;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // this.Shoot();

            Projectile newProjectile = Instantiate(this.projectile, this._origin.position, this._origin.rotation);
            float gravity = newProjectile.Gravity;

            newProjectile.InitiateProjectile(1, this.CalculateVelocity(gravity, this._maxThrowforce, this._origin.position, this._destination.position));
        }
    }

    private void Shoot()
    {
        Projectile newProjectile = Instantiate(this.projectile, this._origin.position, this._origin.rotation);
        float gravity = newProjectile.Gravity;

        Vector3 displacement = new Vector3(this._destination.position.x, this._origin.position.y, this._destination.position.z) - this._origin.position;

        float deltaY = this._destination.position.y - this._origin.position.y;
        float deltaXZ = displacement.magnitude;

        float throwStrength = Mathf.Clamp(Mathf.Sqrt(gravity * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaXZ, 2)))), 0.01f, this._maxThrowforce);
        float angle = Mathf.PI / 2f - (0.5f * (Mathf.PI / 2 - (deltaY / deltaXZ)));

        Vector3 initialVelocity = Mathf.Cos(angle) * throwStrength * displacement.normalized + Mathf.Sin(angle) * throwStrength * Vector3.up;

        newProjectile.InitiateProjectile(1, initialVelocity);
    }

    private Vector3 CalculateVelocity(float gravity, float maxThrowforce, Vector3 origin, Vector3 destination)
    {
        Vector3 displacement = new Vector3(destination.x, origin.y, destination.z) - origin;

        float deltaY = destination.y - origin.y;
        float deltaXZ = displacement.magnitude;

        float throwStrength = Mathf.Clamp(Mathf.Sqrt(gravity * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaXZ, 2)))), 0.01f, maxThrowforce);
        float angle = Mathf.PI / 2f - (0.5f * (Mathf.PI / 2 - (deltaY / deltaXZ)));

        Vector3 initialVelocity = Mathf.Cos(angle) * throwStrength * displacement.normalized + Mathf.Sin(angle) * throwStrength * Vector3.up;
        return initialVelocity;
    }
}
