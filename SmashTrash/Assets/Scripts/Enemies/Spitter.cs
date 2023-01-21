using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] private Transform distanceToTarget;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rangeToRunAway;
    [SerializeField] private float patrollingDistance;
    [SerializeField] private float rangeToAttack;
    [SerializeField] private float patrollingRange;
    //[SerializeField] private GameObject spitWeapon;
    [SerializeField] private Projectile spit;
    [SerializeField] private Transform spitPos;
    [SerializeField] private float MaxThrowForce;
    [SerializeField] private float throwHeight;
    private Rigidbody rb;
    private Rigidbody spitRB;
    private Vector3 nextPos;
    private float Timer = 0;

    enum EnemyState
    {
        Escaping,
        Patrolling
    }

    private EnemyState State;

    //Checks if the player is within a set distance of the Spitter, if it is it tries to keep a set distance from the player
    private void EscapingAI()
    {
        if (rangeToRunAway < Vector3.Distance(transform.position, distanceToTarget.position))
        {
            State = EnemyState.Patrolling;
            nextPos = this.transform.position;
        }
        Vector3 moveDirection = (this.transform.position - this.distanceToTarget.position).normalized;
        rb.velocity = moveDirection * movementSpeed;
        //print("Escaping");
    }


    private void PattrolingAI()
    {
        if (rangeToRunAway > Vector3.Distance(transform.position, distanceToTarget.position))
        {
            State = EnemyState.Escaping;
        }

        if (Vector3.Distance(this.transform.position, nextPos) < 1)
        {
            Vector3 patrollingZoneDistance = ((this.transform.position - this.distanceToTarget.position).normalized);
            Vector3 newPosition = this.distanceToTarget.position + patrollingZoneDistance * patrollingDistance;
            Vector3 newerPosition = new Vector3(newPosition.x + Random.Range(-patrollingRange, patrollingRange), 0.5f, newPosition.z + Random.Range(-patrollingRange, patrollingRange));
            nextPos = newerPosition;
        }

        rb.velocity = (nextPos - this.transform.position).normalized * movementSpeed;
        //print("Patrolling");
    }



    private void throwSpit()
    {
        if (rangeToAttack > Vector3.Distance(transform.position, distanceToTarget.position) && Timer <= 0)
        {   
            Projectile Spit = Instantiate(spit, spitPos.position, Quaternion.identity);
            //Vector3 throwDirection = (distanceToTarget.position - spitPos.position).normalized;
            Vector3 displacement = new Vector3(distanceToTarget.position.x, spitPos.position.y, distanceToTarget.position.z) - spitPos.position;
            float deltaY = distanceToTarget.position.y - spitPos.position.y;
            float deltaXZ = displacement.magnitude;
            print("deltaxZ:" + deltaXZ);
            print("deltaY" + deltaY);
            
            float gravity = Spit.Gravity;
            float throwStrength = Mathf.Clamp(Mathf.Sqrt(gravity * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaXZ, 2)))), 0.01f, MaxThrowForce);
            float angle = Mathf.PI / 2f - (0.5f * (Mathf.PI / 2 - (deltaY / deltaXZ)));
            Vector3 initialVelocity = Mathf.Cos(angle) * throwStrength * displacement.normalized + Mathf.Sign(angle) * throwStrength * Vector3.up;

            //throwDirection.y = throwHeight;
            
            Spit.InitiateProjectile(damage, initialVelocity);
            Timer = 3f;
            print("poop");
        }
        Timer -= Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spitRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case EnemyState.Escaping:
                EscapingAI();
                break;
            case EnemyState.Patrolling:
                PattrolingAI();
                break;
            default:
                break;
        }
        throwSpit();
        this.transform.rotation = Quaternion.LookRotation(this.transform.position - distanceToTarget.transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeToRunAway);
        Vector3 patrollingZoneDistance = ((this.transform.position - this.distanceToTarget.position).normalized);
        Vector3 newPosition = this.distanceToTarget.position + patrollingZoneDistance * patrollingDistance;
        Gizmos.DrawWireSphere(newPosition, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(nextPos, 0.2f);
        Gizmos.DrawWireSphere(transform.position, rangeToAttack);
    }
}
