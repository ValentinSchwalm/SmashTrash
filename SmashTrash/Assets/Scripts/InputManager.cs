using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class InputManager : MonoBehaviour
{
    public UnityEvent onSuckUp, onSuckDown, onInteract, onShoot;
    //private CheckFingerPosition check;
    private int count;

    [Range(0, 1)] [SerializeField] private float skeletonSmoothing;

    [SerializeField] private SkeletonManager skeletonManager;
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private TextMeshProUGUI suck, interact, shoot;
    [SerializeField] private MeshRenderer originMaterial, destinationMaterial;

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
        counter.text = "not entering";
        ManomotionManager.Instance.SetManoMotionSmoothingValue(this.skeletonSmoothing);

        Interact(gesture);
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
            originMaterial.material.color = Color.green;
            destinationMaterial.material.color = Color.green;
            //Instantiate(this.objectToInstantiate, this.skeletonManager._listOfJoints[8].transform.position, Quaternion.identity);
            this.count++;
            this.interact.text = "interact" + this.count;

        }
    }

    void OnSuckDown(GestureInfo gesture)
    {
        ManoGestureContinuous suck = gesture.mano_gesture_continuous;

        // Checks if the current visable hand performs a grab gesture
        if (suck == ManoGestureContinuous.OPEN_HAND_GESTURE)
        {
            onSuckDown.Invoke();
            originMaterial.material.color = Color.yellow;
            destinationMaterial.material.color = Color.yellow;
            //count++;
            //counter.text = count.ToString() + "suck";
            this.suck.text = "sucking";
        }
    }

    void OnSuckUp(GestureInfo gesture)
    {
        ManoGestureContinuous suck = gesture.mano_gesture_continuous;

        // Checks if the current visable hand performs a grab trigger gesture
        if (suck == ManoGestureContinuous.CLOSED_HAND_GESTURE)
        {
            onSuckUp.Invoke();
            originMaterial.material.color = Color.white;
            destinationMaterial.material.color = Color.white;
            this.suck.text = "not sucking";
        }
    }

    void Shoot(GestureInfo gesture)
    {
        ManoGestureContinuous trigger = gesture.mano_gesture_continuous;

        this.counter.text = "entering: " + gesture.mano_gesture_continuous;

        if (trigger == ManoGestureContinuous.POINTER_GESTURE && trigger != ManoGestureContinuous.OPEN_PINCH_GESTURE)
        {
            onShoot.Invoke();
            originMaterial.material.color = Color.red;
            destinationMaterial.material.color = Color.red;
            //count++;
            this.shoot.text = "shooting";
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
}
