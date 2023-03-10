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

    [Range(0, 1)] [SerializeField] private float skeletonSmoothing;

    [SerializeField] private SkeletonManager skeletonManager;
    [SerializeField] private GameObject objectToInstantiate;
    [SerializeField] private MeshRenderer originMaterial, destinationMaterial;


    // Update is called once per frame
    void Update() 
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info; // the current hand gesture being made
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
        }
    }

    void Shoot(GestureInfo gesture)
    {
        ManoGestureContinuous trigger = gesture.mano_gesture_continuous;

        if (trigger == ManoGestureContinuous.POINTER_GESTURE && trigger != ManoGestureContinuous.OPEN_PINCH_GESTURE)
        {
            onShoot.Invoke();
            originMaterial.material.color = Color.red;
            destinationMaterial.material.color = Color.red;
        }
    }
}
