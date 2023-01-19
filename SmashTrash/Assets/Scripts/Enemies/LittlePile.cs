using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittlePile : Enemy
{
    [SerializeField] private float damageRange;
    [SerializeField] private Transform Target;

    enum LPState
    {
        NotExploded,
        Exploded
    }

    private LPState State;

    private void DamageOnImpact() 
    {
        
    }

    //Checks if the player is within activation distance, if it is the Little Pile explodes
    private void CheckIfActive()
    {
        if (damageRange > Vector3.Distance(transform.position,  Target.position))
        {
            State = LPState.Exploded;
        }
        //print("NotExploded");
    }

    //Pretty self explanitory Little Pile explodes deleting it (the gameObject) and hurting the player
    private void Explode()
    {
        Destroy(gameObject);
        //Player recieves damage?
        print("BOOM");
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
            case LPState.NotExploded:
                CheckIfActive();
                break;
            case LPState.Exploded:
                Explode();
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
