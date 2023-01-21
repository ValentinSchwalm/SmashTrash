using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class InputManager : MonoBehaviour
{
    public UnityEvent onSuck, onInteract, onShoot;
    private GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info; // the current hand gesture being made
    private CheckFingerPosition check;

    //[SerializeField] private GameObject[] handPoints;
    [SerializeField] private SkeletonManager skeletonManager;
    [SerializeField] private GameObject objectToInstantiate;

    // Update is called once per frame
    void Update()
    {   
        Interact();
        Suck();
        // this.ShowSkeleton();
    }
  
    void Interact()
    {
        ManoGestureTrigger trigger = gesture.mano_gesture_trigger;

        // Checks if the current visable hand performs a click trigger gesture
        if (trigger == ManoGestureTrigger.CLICK)
        {
            //onInteract.Invoke();
            Instantiate(this.objectToInstantiate, this.skeletonManager._listOfJoints[8].transform.position, Quaternion.identity);
        }
    }
    
    void Suck()
    {
        ManoGestureContinuous suck = gesture.mano_gesture_continuous;
        ManoGestureTrigger release = gesture.mano_gesture_trigger;

        // Method that will run while the selected ManoGestureContinuous is performed.

        if (suck == ManoGestureContinuous.CLOSED_HAND_GESTURE)
        {
            if (release == ManoGestureTrigger.RELEASE_GESTURE)
            {
                //onSuck.Invoke();
                Handheld.Vibrate();
            }
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
