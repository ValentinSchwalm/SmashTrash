using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    [SerializeField] private Finger[] bentFingers;
    [SerializeField] private Finger[] stretchedFingers;

    private bool CheckPistolGesture()
    {
        // Check bent fingers
        foreach (Finger finger in this.bentFingers)
        {
            float distance = Vector3.Distance(finger.startPoint.position, finger.endPoint.position);

            if (distance < finger.range)
            {
                return false;
            }
        }

        // Check stretched fingers
        foreach (Finger finger in this.stretchedFingers)
        {
            float distance = Vector3.Distance(finger.startPoint.position, finger.endPoint.position);

            if (distance < finger.range)
            {
                return false;
            }
        }

        // Return true if all fingers have been checked correctly
        return true;
    }

    [System.Serializable]
    public struct Finger
    {
        public Transform endPoint;  // Fingertip
        public Transform startPoint;  // Palm
        public float range;
    }
}
