using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rangeToAttack;
    [SerializeField] private Transform targetToChase;

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

    }

    protected virtual void AttackState()
    {

    }

    public void Attack()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case EnemyState.Chase:
                ChaseAI();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            default:
                break;
        }
    }
}
