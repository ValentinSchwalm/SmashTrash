using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePile : Enemy
{
    [SerializeField] private float damageRange;
    [SerializeField] private Transform Target;
    [SerializeField] private LayerMask playerMask;
    private List<GameObject> hitObjects = new List<GameObject>();
    private float TimeBetweenDamage;

    enum LPState
    {
        NotHurting,
        Hurting
    }

    private LPState State;


    //Checks if the player is within activation distance, if it is the Little Pile starts hurting the player
    private void NotHurting()
    {
        if (damageRange > Vector3.Distance(transform.position,  Target.position))
        {
            State = LPState.Hurting;
        }
        
    }

    //Pretty self explanitory Little Pile hurts the player if the player is near to it
    private void Hurting()
    {
        if (damageRange < Vector3.Distance(transform.position, Target.position))
        {
            State = LPState.NotHurting;
        }
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, damageRange, playerMask);

        foreach (var item in playerCollider)
        {
            IHealthSystem healthSystem = item.GetComponent<IHealthSystem>();

            if (healthSystem != null && !hitObjects.Contains(item.gameObject))
            {
                healthSystem.ReceiveDamage(this.damage);
                hitObjects.Add(item.gameObject);
                TimeBetweenDamage = 0.5f;
            }
        }

        if (TimeBetweenDamage < 0)
        {
            hitObjects.Clear();
        }

        TimeBetweenDamage -= Time.deltaTime;
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
            case LPState.NotHurting:
                NotHurting();
                break;
            case LPState.Hurting:
                Hurting();
                break;
            default:
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }
}
