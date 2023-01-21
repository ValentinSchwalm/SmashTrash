using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    #region Variables

    [Header("Projectile Stats")]
    [SerializeField] private float gravity;
    [SerializeField] private float maxGravity;
    [SerializeField] private float timeToLive;
    private int damage;

    [Header("Collisions")]
    [SerializeField] private float hitRadius;
    [SerializeField] private LayerMask hitMask;

    private Vector3 velocity;
    private Rigidbody rb;

    #endregion

    public float Gravity { get { return this.gravity; } }

    #region Unity Methods

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.hitRadius);
    }

    private void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Update velocity
        this.velocity.y -= this.gravity * Time.deltaTime;
        this.rb.velocity = this.velocity;

        // Check if the projectile hit something
        Collider[] hitObjects = Physics.OverlapSphere(this.transform.position, this.hitRadius, this.hitMask);

        if (hitObjects.Length == 0)
        {
            return;
        }

        foreach (Collider item in hitObjects)
        {
            IHealthSystem healthSystem = item.GetComponent<IHealthSystem>();

            if (healthSystem != null)
            {
                healthSystem.ReceiveDamage(this.damage);
            }
        }

        Destroy(this.gameObject);
    }

    #endregion

    #region Projectile Methods

    /// <summary>
    /// Initiates the starting values of the projectile.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="initialVelocity"></param>
    public void InitiateProjectile(int damage, Vector3 initialVelocity)
    {
        this.damage = damage;
        this.velocity = initialVelocity;
    }

    #endregion
}
