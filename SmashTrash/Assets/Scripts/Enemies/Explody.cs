using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explody : ChasingEnemy
{
    [SerializeField] private float ExplosionRange;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private AudioClip audio;
    private List<GameObject> hitObjects = new List<GameObject>();
    private float Timer;

    private void Explode()
    {
        Collider[] playerCollider = Physics.OverlapSphere(this.transform.position, ExplosionRange, playerMask);
        print("before");
        foreach (var item in playerCollider)
        {
            IHealthSystem healthSystem = item.GetComponent<IHealthSystem>();
            print("Boom");
            if (healthSystem != null && !hitObjects.Contains(item.gameObject))
            {
                healthSystem.ReceiveDamage(this.damage);
                hitObjects.Add(item.gameObject);
                AudioSource.PlayClipAtPoint(audio, transform.position);
                Destroy(gameObject);
                Timer = 3f;
                
            }
        }
        if (Timer < 0)
        {
            hitObjects.Clear();
        }
        Timer = -Time.deltaTime;
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void AttackState()
    {
        base.AttackState();
        Explode();
        
    }
    protected override void Update()
    {
        base.Update();
    }
}
