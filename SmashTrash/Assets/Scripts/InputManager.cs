using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class InputManager : MonoBehaviour
{
    public UnityEvent onSuck, onInteract, onShoot;
    //private CheckFingerPosition check;
    private int count;

    [SerializeField] private SkeletonManager skeletonManager;
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private TextMeshProUGUI counter;
   

    // Update is called once per frame
    void Update() 
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info; // the current hand gesture being made
        Interact(gesture);
        Suck(gesture);
        // this.ShowSkeleton();
    }
  
    void Interact(GestureInfo gesture)
    {
        ManoGestureTrigger trigger = gesture.mano_gesture_trigger;

        // Checks if the current visable hand performs a click trigger gesture
        if (trigger == ManoGestureTrigger.CLICK)
        {
            //onInteract.Invoke();
            Instantiate(this.objectToInstantiate, this.skeletonManager._listOfJoints[8].transform.position, Quaternion.identity);
        }
    }
    
    void Suck(GestureInfo gesture)
    {
        ManoGestureContinuous suck = gesture.mano_gesture_continuous;


        // Checks if the current visable hand performs a grab trigger gesture
        if (suck == ManoGestureContinuous.CLOSED_HAND_GESTURE)
        {
            //onSuck.Invoke();
            //count++;
            //counter.text = count.ToString() + "suck";
        }
    }
    /*
    void Shoot()
    {
        if ()
        {
            onShoot.Invoke();
        }
    }

    /*
   void ShowSkeleton()
   {
       print("test");
       for (int i = 0; i < this.skeletonManager._listOfJoints.Count; i++)
       {
           if (i > this.handPoints.Length)
           {
               break;
           }

           this.handPoints[i].transform.position = this.skeletonManager._listOfJoints[i].transform.position;
       }
   }
   */

}
