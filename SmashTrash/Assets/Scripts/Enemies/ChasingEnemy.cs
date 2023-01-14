using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rangeToAttack;
    [SerializeField] private Transform targetToChase;
    private Rigidbody rigidbody;

    enum EnemyState
    {
        Chase,
        Attack
    }

    private EnemyState State;

    public void ChaseAI()
    {
        
    }

    private void ChaseState()
    {
        if(rangeToAttack > Vector3.Distance(transform.position, targetToChase.position))
        {
            State = EnemyState.Attack;
        }
        Vector3 moveDirection = (this.targetToChase.position - this.transform.position).normalized;
        //rigidbody.MovePosition(targetToChase.position);
        //rigidbody.AddForce(moveDirection * movementSpeed * Time.deltaTime);
        rigidbody.velocity = moveDirection * movementSpeed * Time.deltaTime;
        print("Chase");
    }

    protected virtual void AttackState()
    {
        if (rangeToAttack < Vector3.Distance(transform.position, targetToChase.position))
        {
            State = EnemyState.Chase;
        }
        print("Attack");
    }

    public void Attack()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case EnemyState.Chase:
                ChaseState();
                break;
            case EnemyState.Attack:
                AttackState();
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