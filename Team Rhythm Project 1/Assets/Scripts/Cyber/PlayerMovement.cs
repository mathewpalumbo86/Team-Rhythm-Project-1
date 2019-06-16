using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Will store the controller rotation
    public Quaternion controllerGO;

    // Controller values shown in UI overlay
    public Text xText;
    public Text yText;
    public Text zText;

    // Update is called once per frame
    void Update()
    {
        // Get the controller rotation
        controllerGO = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);

        // Converts the quaternion to readable values
        Vector3 angles = controllerGO.eulerAngles;

        // Updaating the text overlay
        xText.text = "X: " + angles.x;
        yText.text = "Y: " + angles.y; 
        zText.text = "Z: " + angles.z; 


        // xText.text = controllerGO.rotation.eulerAngles.x;





        // Convert rotation to either a left, right or middle position?
        // Restrictions:
        // can only move in a straight line between 2 points
        // can move either way or pause in a position
        // 


        // Update the players position




    }
}
