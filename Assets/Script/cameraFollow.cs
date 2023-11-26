using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraFollow : MonoBehaviour
{
    GameObject marble;
    float xOffset;
    float yOffset;
    float zOffset;
    float travelTime = 5;
    //Camera[] cameras;
   // PlayerInput controls;
    //InputAction pan_left;
    //InputAction pan_right;
    //int cameraNum = 0;
    //int maxCameras = 1;

    void Start()
    {
        //cameras = FindObjectsOfType<Camera>();
        marble = GameObject.Find("Marble");
        /*controls = GetComponent<PlayerInput>();
        pan_left = controls.actions.FindAction("Pan Camera Left");
        pan_left.Enable();
        pan_right = controls.actions.FindAction("Pan Camera Right");
        pan_right.Enable();*/

        if (marble == null)
        {
            Debug.Log("Vital components missing.");
        }
        xOffset = transform.position.x - marble.transform.position.x;
        yOffset = transform.position.y - marble.transform.position.y;
        zOffset = transform.position.z - marble.transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(marble.transform.position.x + xOffset,
                            marble.transform.position.y + yOffset,
                            marble.transform.position.z + zOffset);

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime + travelTime);
    }
}
