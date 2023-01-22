using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class CheckFingerPosition : MonoBehaviour
{
    private static SkeletonManager skeletonManager;
    [SerializeField] private Finger thumb;
    [SerializeField] private Finger index;
    [SerializeField] private Finger middle;
    [SerializeField] private Finger ring;
    [SerializeField] private Finger pinkie;
    [SerializeField] private Finger[] all;
    [SerializeField] private Finger[] bent;
    [SerializeField] private Finger[] stretched;

    private void Start()
    {
        // Inizialize Skeleton Mmanager
        skeletonManager = FindObjectOfType<SkeletonManager>();
    }

    void Update()
    {
        thumb.SetFinger(1, 4, 1);
        //thumb.startPoint.position = skeletonManager._listOfJoints[1].transform.position;
        //thumb.endPoint.position = skeletonManager._listOfJoints[4].transform.position;
        float distance = Vector3.Distance(thumb.startPoint.position, thumb.endPoint.position);
        print(distance);
    }

    void Initialize()
    {
        
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

        public void SetFinger(int start, int end, float range)
        {
            startPoint = skeletonManager._listOfJoints[start].transform;
            endPoint = skeletonManager._listOfJoints[end].transform;
            this.range = range;
        }
    }
}
