using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class InputManager : MonoBehaviour
{
    public UnityEvent onSuckUp, onSuckDown, onInteract, onShoot;
    //private CheckFingerPosition check;
    private int count;

    [SerializeField] private SkeletonManager skeletonManager;
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private TextMeshProUGUI counter;

    [SerializeField] private TextMeshProUGUI suck, interact, shoot;

    private void Start()
    {
        this.interact.text = "interact" + this.count;
        this.suck.text = "not sucking";
    }


    // Update is called once per frame
    void Update() 
    {
        this.shoot.text = "not shooting";
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info; // the current hand gesture being made

        this.interact.text = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.ToString();
        this.shoot.text = ManomotionManager.Instance.ToString();

        Interact(gesture);
        //Suck(gesture);
        Shoot(gesture);
        OnSuckDown(gesture);
        OnSuckUp(gesture);
    }
  
    void Interact(GestureInfo gesture)
    {
        ManoGestureTrigger interaction = gesture.mano_gesture_trigger;

        // Checks if the current visable hand performs a click trigger gesture
        if (interaction == ManoGestureTrigger.CLICK)
        {
            onInteract.Invoke();
            //Instantiate(this.objectToInstantiate, this.skeletonManager._listOfJoints[8].transform.position, Quaternion.identity);
            this.count++;
            this.interact.text = "interact" + this.count;
        }
    }
    
    //void Suck(GestureInfo gesture)
    //{
    //    ManoGestureContinuous suck = gesture.mano_gesture_continuous;


    //    // Checks if the current visable hand performs a grab trigger gesture
    //    if (suck == ManoGestureContinuous.CLOSED_HAND_GESTURE)
    //    {
    //        onSuck.Invoke();
    //        //count++;
    //        //counter.text = count.ToString() + "suck";
    //    }
    //}

    void OnSuckDown(GestureInfo gesture)
    {
        ManoGestureTrigger suck = gesture.mano_gesture_trigger;

        // Checks if the current visable hand performs a grab trigger gesture
        if (suck == ManoGestureTrigger.GRAB_GESTURE && gesture.hand_side == HandSide.Backside)
        {
            onSuckDown.Invoke();
            //count++;
            //counter.text = count.ToString() + "suck";
            this.suck.text = "sucking";

        }
    }

    void OnSuckUp(GestureInfo gesture)
    {
        ManoGestureTrigger suck = gesture.mano_gesture_trigger;

        // Checks if the current visable hand performs a grab trigger gesture
        if (suck == ManoGestureTrigger.RELEASE_GESTURE && gesture.hand_side == HandSide.Backside)
        {
            onSuckUp.Invoke();
            this.suck.text = "not sucking";
        }
    }

    void Shoot(GestureInfo gesture)
    {
        ManoGestureContinuous trigger = gesture.mano_gesture_continuous;

        if (trigger == ManoGestureContinuous.POINTER_GESTURE && trigger != ManoGestureContinuous.OPEN_PINCH_GESTURE)
        {
            onShoot.Invoke();
            //count++;
            this.shoot.text = "shooting";
        }
    }
}
