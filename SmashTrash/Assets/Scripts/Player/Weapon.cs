using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables

    [Header("Input")]
    [SerializeField] private Transform originPoint;
    [SerializeField] private Transform destinationPoint;

    [Header("Shoot Stats")]
    [SerializeField] private int damage;
    [SerializeField] private float shootingForce;
    [SerializeField] private float cooldownValue;
    private float cooldown;
    [SerializeField] private int maxAmmunition;
    private int ammunition;
    [SerializeField] private Projectile projectile;

    [Header("Suck Stats")]   
    [SerializeField] private int suckForce;
    [SerializeField] private float suckRadius;
    [SerializeField] private Vector3 suckSize;
    [SerializeField] private Transform suckTransform;
    [SerializeField] private LayerMask suckMask;
    private GameObject[] suckedTrash;

    #endregion

    #region Unity Methods

    private void OnDrawGizmos()
    {
        Vector3 suckpos = this.destinationPoint.position + (this.destinationPoint.position - this.originPoint.position).normalized * this.suckSize.z / 2;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(suckpos, 0.1f);
        Gizmos.DrawWireSphere(this.destinationPoint.position, this.suckRadius);

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(this.suckTransform.position, this.suckTransform.rotation, this.suckTransform.lossyScale);
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(Vector3.zero, this.suckSize);
    }

    private void Start()
    {
        // Initialize ammunition
        this.ammunition = this.maxAmmunition;
    }

    private void Update()
    {
        if (this.cooldown > 0)
        {
            this.cooldown -= Time.deltaTime;
        }
    }

    #endregion

    #region Weapon Methods

    public void Shoot()
    {
        if (this.ammunition <= 0 || this.cooldown > 0)
        {
            return;
        }

        // Shoot
        Vector3 initialVelocity = (this.destinationPoint.position - this.originPoint.position).normalized;
        initialVelocity *= this.shootingForce;

        Projectile projectile = Instantiate(this.projectile, this.destinationPoint.position, Quaternion.identity);
        projectile.InitiateProjectile(this.damage, initialVelocity);

        this.cooldown = this.cooldownValue;
        this.ammunition--;
    }

    public void Suck()
    {
        if (this.ammunition >= this.maxAmmunition)
        {
            return;
        }

        Vector3 suckDirection = (this.destinationPoint.position - this.originPoint.position).normalized;
        Vector3 suckpos = this.destinationPoint.position + suckDirection * this.suckSize.z / 2;
        this.suckTransform.position = suckpos;
        this.suckTransform.rotation = Quaternion.LookRotation(suckDirection, Vector3.forward);

        Collider[] objectsToSuck = Physics.OverlapBox(this.suckTransform.position, this.suckSize / 2, this.suckTransform.rotation, this.suckMask);

        foreach (Collider item in objectsToSuck)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce((this.destinationPoint.position - rb.position).normalized * this.suckForce * Time.deltaTime, ForceMode.VelocityChange);
            }
        }

        Collider[] suckedObjects = Physics.OverlapSphere(this.destinationPoint.position, this.suckRadius, this.suckMask);
        foreach (Collider item in suckedObjects)
        {
            this.ammunition++;
            Destroy(item.gameObject);
        }
    }

    #endregion
}
