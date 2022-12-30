using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGesture : MonoBehaviour
{
    public ManoGestureContinuous continuousGesture;
    private GestureInfo gesture; // the current hand gesture being made

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;

        // RecognizeInteraction(gesture);
        // RecignizeSuck(gesture);

    }

    
  
    void RecognizeInteraction(GestureInfo gesture)
    {
        // Checks if the current visable hand performs a gesture from pinch family
        // if so, then the code will be executed in this case the phone will vibrate.

        if (gesture.mano_class == ManoClass.PINCH_GESTURE)
        {
            // Your code here
            Handheld.Vibrate();
        }
    }

    void RecignizeSuck(GestureInfo gesture)
    {

        // Method that will run while the selected ManoGestureContinuous is performed.

        if (gesture.mano_gesture_continuous == continuousGesture)
        {
            // Code that will execute while continuous gesture is performed
        }
    }
}
