using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : ChasingEnemy
{
    [SerializeField] private GameObject hammerWeapon;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Animator animator;
    private List<GameObject> hitObjects = new List<GameObject>();
    private float Timer;

    private void HammerDamage()
    {
        Collider[] playerCollider = Physics.OverlapSphere(hammerWeapon.transform.position, damageRadius, playerMask);
        foreach (var item in playerCollider)
        {
            IHealthSystem healthSystem = item.GetComponent<IHealthSystem>();

            if (healthSystem != null && !hitObjects.Contains(item.gameObject))
            {
                healthSystem.ReceiveDamage(this.damage);
                hitObjects.Add(item.gameObject);
                Timer = 1.5f;
            }
            
        }
        if (Timer < 0)
        {
            hitObjects.Clear();
        }
        Timer -= Time.deltaTime;
    }

    protected override void AttackState()
    {
        base.AttackState();
        animator.SetBool("Attack", true);
        HammerDamage();
    }

    protected override void ChaseState()
    {
        base.ChaseState();
        animator.SetBool("Attack", false);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(hammerWeapon.transform.position, damageRadius);
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
}
