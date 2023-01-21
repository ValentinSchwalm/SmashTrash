using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFingerPosition 
{
    [SerializeField] private SkeletonManager skeletonManager;
    [SerializeField] private Finger thumb;
    [SerializeField] private Finger index;
    [SerializeField] private Finger middle;
    [SerializeField] private Finger ring;
    [SerializeField] private Finger pinkie;
    [SerializeField] private Finger[] all = new Finger[4];
    [SerializeField] private Finger[] bent;
    [SerializeField] private Finger[] stretched;

    void Initialize()
    {
        thumb.startPoint.position = skeletonManager._listOfJoints[1].transform.position;
        thumb.endPoint.position = skeletonManager._listOfJoints[4].transform.position;
        index.startPoint.position = skeletonManager._listOfJoints[5].transform.position;
        index.endPoint.position = skeletonManager._listOfJoints[8].transform.position;
        middle.startPoint.position = skeletonManager._listOfJoints[9].transform.position;
        middle.endPoint.position = skeletonManager._listOfJoints[12].transform.position;
        ring.startPoint.position = skeletonManager._listOfJoints[13].transform.position;
        ring.endPoint.position = skeletonManager._listOfJoints[16].transform.position;
        pinkie.startPoint.position = skeletonManager._listOfJoints[17].transform.position;
        pinkie.endPoint.position = skeletonManager._listOfJoints[20].transform.position;
       
        

    }
    private bool CheckPistolGesture()
    {
        // Check bent fingers
        foreach (Finger finger in this.bent)
        {
            float distance = Vector3.Distance(finger.startPoint.position, finger.endPoint.position);

            if (distance < finger.range)
            {
                return false;
            }
        }

        // Check stretched fingers
        foreach (Finger finger in this.stretched)
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
        public Transform startPoint;  // Palm
        public Transform endPoint;  // Fingertip
        public float range;

        void SetFinger(Transform startPoint, Transform endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.range = 1;
        }
    }
}
