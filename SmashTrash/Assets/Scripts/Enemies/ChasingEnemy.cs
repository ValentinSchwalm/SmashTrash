using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rangeToAttack;
    [SerializeField] private Transform targetToChase;
    private Rigidbody rigidbody;
    [SerializeField] private Animator animator;

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
            animator.SetBool("Attack", true);
        }
        Vector3 moveDirection = (this.targetToChase.position - this.transform.position).normalized;
        //rigidbody.MovePosition(targetToChase.position);
        //rigidbody.AddForce(moveDirection * movementSpeed * Time.deltaTime);
        rigidbody.velocity = moveDirection * movementSpeed;
        //print("Chase");
    }

    protected virtual void AttackState()
    {
        if (rangeToAttack < Vector3.Distance(transform.position, targetToChase.position))
        {
            State = EnemyState.Chase;
            animator.SetBool("Attack", false);
        }
        //print("Attack");
    }

    public void Attack()
    {

    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
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

        this.transform.rotation = Quaternion.LookRotation(this.transform.position - targetToChase.transform.position);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeToAttack);
    }
}