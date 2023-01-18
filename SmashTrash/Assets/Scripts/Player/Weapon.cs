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
    [SerializeField] private float suckTime;
    [SerializeField] private float suckRadius;
    [SerializeField] private Vector3 suckSize;
    [SerializeField] private Transform suckTransform;
    [SerializeField] private LayerMask suckMask;
    private List<Trash> suckedTrash = new List<Trash>();

    [Header("Bezier Curve")]
    [SerializeField] private Transform[] bezierTransforms;
    [SerializeField] private float offsetPoint1;
    [SerializeField] private float offsetPoint2;

    #endregion

    #region Unity Methods

    private void OnDrawGizmos()
    {
        // Draw Bezier Curve
        if (this.bezierTransforms.Length == 4)
        {
            Vector3 gizmoPos;

            for (float t = 0; t <= 1; t += 0.02f)
            {
                gizmoPos = Mathf.Pow(1 - t, 3) * this.bezierTransforms[0].position +
                    3 * Mathf.Pow(1 - t, 2) * t * this.bezierTransforms[1].position +
                    3 * (1 - t) * Mathf.Pow(t, 2) * this.bezierTransforms[2].position +
                    Mathf.Pow(t, 3) * this.bezierTransforms[3].position;

                Gizmos.DrawSphere(gizmoPos, 0.05f);
            }
        }

        // Draw Suck Position
        Vector3 suckpos = this.destinationPoint.position + (this.destinationPoint.position - this.originPoint.position).normalized * this.suckSize.z / 2;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(suckpos, 0.1f);
        Gizmos.DrawWireSphere(this.destinationPoint.position, this.suckRadius);

        // Draw Suck Box
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

    #region Properties

    public int Damage
    { 
        get { return this.damage; } 
        set { this.damage = value; }
    }

    public int MaxAmmunition
    {
        get { return this.maxAmmunition; }
        set { this.maxAmmunition = value; }
    }

    public float ShootingForce
    {
        get { return this.shootingForce; } 
        set { this.shootingForce = value; }
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

    public void OnSuckStop()
    {
        this.suckedTrash.Clear();
    }

    public void Suck()
    {
        if (this.ammunition >= this.maxAmmunition)
        {
            return;
        }

        this.DetectTrash();
        this.SuckTrash();
    }

    private void DetectTrash()
    {
        Vector3 suckDirection = (this.destinationPoint.position - this.originPoint.position).normalized;
        Vector3 suckpos = this.destinationPoint.position + suckDirection * this.suckSize.z / 2;
        this.suckTransform.position = suckpos;
        this.suckTransform.rotation = Quaternion.LookRotation(suckDirection, Vector3.forward);

        Collider[] objectsToSuck = Physics.OverlapBox(this.suckTransform.position, this.suckSize / 2, this.suckTransform.rotation, this.suckMask);

        foreach (Collider item in objectsToSuck)
        {
            Trash trash = item.GetComponent<Trash>();

            if (trash != null)
            {
                if (!this.suckedTrash.Contains(trash))
                {
                    this.suckedTrash.Add(trash);

                    this.bezierTransforms[0].position = trash.transform.position;
                    this.bezierTransforms[3].position = this.destinationPoint.position;

                    Vector3 direction = this.bezierTransforms[3].position - this.bezierTransforms[0].position;
                    this.bezierTransforms[0].rotation = Quaternion.LookRotation(direction, Vector3.forward);

                    Vector3 offset = (this.bezierTransforms[0].right * Random.Range(-1f, 1f) + this.bezierTransforms[0].up * Random.Range(-1f, 1f)).normalized;

                    this.bezierTransforms[1].position = (this.bezierTransforms[0].position + direction * 0.1f) + offset * this.offsetPoint1;
                    this.bezierTransforms[2].position = (this.bezierTransforms[0].position + direction * 0.5f) + offset * this.offsetPoint2;

                    trash.SetTrashCurve(this.bezierTransforms[0].position, this.bezierTransforms[1].position, this.bezierTransforms[2].position);
                    trash.Timer = 0;
                }
            }
        }
    }

    private void SuckTrash()
    {
        List<Trash> suckedInTrash = new List<Trash>();

        foreach (Trash trash in this.suckedTrash)
        {
                Vector3 newPosition = Mathf.Pow(1 - trash.Timer, 3) * trash.Origin +
            3 * Mathf.Pow(1 - trash.Timer, 2) * trash.Timer * trash.Bezier1 +
            3 * (1 - trash.Timer) * Mathf.Pow(trash.Timer, 2) * trash.Bezier2 +
            Mathf.Pow(trash.Timer, 3) * this.destinationPoint.position;

            Debug.DrawRay(newPosition, Vector3.up, Color.red);


            trash.transform.position = newPosition;
            trash.Timer += Time.deltaTime * this.suckTime;

            if (trash.Timer >= 1)
            {
                suckedInTrash.Add(trash);
            }
        }

        for (int i = 0; i < suckedInTrash.Count; i++)
        {
            Destroy(suckedInTrash[0].gameObject);
            this.suckedTrash.Remove(suckedInTrash[0]);
            suckedInTrash.Remove(suckedInTrash[0]);
            this.ammunition++;
        }
    }

    #endregion
}
