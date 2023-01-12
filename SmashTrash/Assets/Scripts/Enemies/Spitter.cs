using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] private Transform distanceToTarget;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rangeToAttack;
    private Rigidbody rb;

    enum EnemyState
    {
        Escaping,
        Patrolling
    }

    private EnemyState State;

    private void EscapingAI()
    {
        if (rangeToAttack < Vector3.Distance(transform.position, distanceToTarget.position))
        {
            State = EnemyState.Patrolling;
        }
        Vector3 moveDirection = (this.transform.position - this.distanceToTarget.position).normalized;
        rb.velocity = moveDirection * movementSpeed * Time.deltaTime;
        print("Escaping");
    }


    private void PattrolingAI()
    {
        if (rangeToAttack > Vector3.Distance(transform.position, distanceToTarget.position))
        {
            State = EnemyState.Escaping;
        }
        Vector3 patrollingZoneDistance = ((this.distanceToTarget.position  - this.transform.position) / 2);
        
        print("Patrolling");
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
        Gizmos.DrawWireSphere(transform.position, rangeToAttack);
    }
}
