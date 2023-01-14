using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public int count;

    [SerializeField] private GameObject[] handPoints;
    [SerializeField] private SkeletonManager skeletonManager;

    [SerializeField] private GameObject objectToInstantiate;

    // Update is called once per frame
    void Update()
    {
        
        RecognizeInteraction();

        this.ShowSkeleton();
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

            Instantiate(this.objectToInstantiate, this.skeletonManager._listOfJoints[8].transform.position, Quaternion.identity);
        }
    }

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
