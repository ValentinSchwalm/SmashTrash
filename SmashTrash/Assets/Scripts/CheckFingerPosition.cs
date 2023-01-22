using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using TMPro;

public class CheckFingerPosition : MonoBehaviour
{
    private static SkeletonManager skeletonManager;
    [SerializeField] private Finger thumb;
    [SerializeField] private Finger index;
    [SerializeField] private Finger middle;
    [SerializeField] private Finger ring;
    [SerializeField] private Finger pinkie;
    [SerializeField] public Finger[] bent;
    [SerializeField] public Finger[] stretched;
    [SerializeField] public TextMeshProUGUI stretched_;
    [SerializeField] public TextMeshProUGUI bent_;
    [SerializeField] public TextMeshProUGUI shoot;

    private void Start()
    {
        // Inizialize Skeleton Mmanager
        skeletonManager = FindObjectOfType<SkeletonManager>();
    }

    void Update()
    {
        stretched = new Finger[2];
        bent = new Finger[3];

        stretched[0] = thumb;
        stretched[1] = index;
        bent[0] = middle;
        bent[1] = ring;
        bent[2] = pinkie;
        
        thumb.SetFinger(1, 4, 1.0);
        index.SetFinger(5, 8, 1.0);
        middle.SetFinger(9, 12, 0.3);
        ring.SetFinger(13, 16, 0.3);
        pinkie.SetFinger(17, 20, 0.3);

        CheckPistolGesture();
    }

    void Initialize()
    {
        
    }


    private bool CheckPistolGesture()
    {
        shoot.text = "not shooting!";


        // Check bent fingers
        foreach (Finger finger in this.bent)
        {
            float distance = Vector3.Distance(finger.startPoint.position, finger.endPoint.position);
           
            if (distance > finger.range)
            {
                bent_.text = "finger is not bent";
                return false;
            }
        }

        // Check stretched fingers
        foreach (Finger finger in this.stretched)
        {
            float distance = Vector3.Distance(finger.startPoint.position, finger.endPoint.position);
            
            if (distance < finger.range)
            {
                stretched_.text = "finger is not stretched";
                return false;
            }
        }

        // Return true if all fingers have been checked correctly
        shoot.text = "shoot!";
        return true;
    }

    [System.Serializable]
    public struct Finger
    {
        public Transform startPoint;  // Palm
        public Transform endPoint;  // Fingertip
        public double range;

        public void SetFinger(int start, int end, double range)
        {
            startPoint = skeletonManager._listOfJoints[start].transform;
            endPoint = skeletonManager._listOfJoints[end].transform;
            this.range = range;
        }
    }
}
