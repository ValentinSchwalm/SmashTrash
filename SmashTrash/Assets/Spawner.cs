using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ObjectToSpawn[] objectsToSpawn;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private ARRaycastManager raycastManager;

    private void Start()
    {
        foreach (ObjectToSpawn item in this.objectsToSpawn)
        {
            Instantiate(item.objectToSpawn, item.transformToSpawn.position, item.transformToSpawn.rotation);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            List<ARRaycastHit> touches = new List<ARRaycastHit>();
            this.raycastManager.Raycast(Input.GetTouch(0).position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (touches.Count > 0)
            {
                Instantiate(this.objectToSpawn, touches[0].pose.position, touches[0].pose.rotation);
            }
        }
    }


    [System.Serializable]
    public struct ObjectToSpawn
    {
        public GameObject objectToSpawn;
        public Transform transformToSpawn;
    }
}
