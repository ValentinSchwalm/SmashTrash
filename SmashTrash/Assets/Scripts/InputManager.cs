using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public int count; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RecognizeInteraction();
        // RecignizeSuck(gesture);

    }
  
    private void RecognizeInteraction()
    {
        // public ManoGestureContinuous continuousGesture;
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info; // the current hand gesture being made
        ManoGestureTrigger trigger = gesture.mano_gesture_trigger;

        // Checks if the current visable hand performs a gesture from pointer family & if detected side of hand is the backside
        // if so, then the code will be executed; in this case the phone will vibrate.

        if (trigger == ManoGestureTrigger.CLICK)
        {
            Handheld.Vibrate();
            count++;
            counter.text = count.ToString();
        }
    }

    void ShowSkeleton()
    {

    }
    /*
    void RecignizeSuck(GestureInfo gesture)
    {

        // Method that will run while the selected ManoGestureContinuous is performed.

        if (gesture.mano_gesture_continuous == continuousGesture)
        {
            // Code that will execute while continuous gesture is performed
        }
    }
    */
}
