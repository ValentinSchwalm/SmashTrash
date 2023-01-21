using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CheckFingerPosition : MonoBehaviour
{
    [SerializeField] public static SkeletonManager skeletonManager;
    [SerializeField] private Finger thumb;
    [SerializeField] private Finger index;
    [SerializeField] private Finger middle;
    [SerializeField] private Finger ring;
    [SerializeField] private Finger pinkie;
    [SerializeField] private Finger[] all;
    [SerializeField] private Finger[] bent;
    [SerializeField] private Finger[] stretched;


    void Initialize()
    {
        thumb.SetFinger(1, 4, 1);
        float distance = Vector3.Distance(thumb.startPoint.position, thumb.endPoint.position);
        print(distance);
    }


    private bool CheckPistolGesture()
    {
        // Check bent fingers
        foreach (Finger finger in this.bent)
        {
            float distance = Vector3.Distance(finger.startPoint.position, finger.endPoint.position);
            Console.WriteLine(distance);
            if (distance < finger.range)
            {
                return false;
            }
        }

        // Check stretched fingers
        foreach (Finger finger in this.stretched)
        {
            float distance = Vector3.Distance(finger.startPoint.position, finger.endPoint.position);
            Console.WriteLine(distance);
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
        public Transform startPoint;  // Palm
        public Transform endPoint;  // Fingertip
        public float range;

        public void SetFinger(int startPoint, int endPoint, float range)
        {
            this.startPoint.position = skeletonManager._listOfJoints[startPoint].transform.position;
            this.endPoint.position = skeletonManager._listOfJoints[endPoint].transform.position; ;
            this.range = range;
        }

}
}
