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
    private Rigidbody rb;
    private Vector3 nextPos;

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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
