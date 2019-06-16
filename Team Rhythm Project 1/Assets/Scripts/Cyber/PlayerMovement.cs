using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// To be attached to the player vehicle.

public class PlayerMovement : MonoBehaviour
{
    // Will store the controller rotation
    public Quaternion controllerGO;

    // Controller values shown in UI overlay
    public Text xText;
    public Text yText;
    public Text zText;
    // Used to indicate if the controller is pointing forward, left or right.
    public Text controllerDirectionText;

    // Movement targets
    public Transform leftTarget;
    public Transform rightTarget;


    // The speed the vehicle should move.
    public float speed = 1.0f;


    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        // calculate distance to move
        float step = speed * Time.deltaTime; 



        // Get the controller rotation
        controllerGO = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);

        // Converts the quaternion to readable values
        Vector3 angles = controllerGO.eulerAngles;

        // Updating the text overlay
        xText.text = "X: " + angles.x;
        yText.text = "Y: " + angles.y; 
        zText.text = "Z: " + angles.z;

        // Checks the controller y rotation and assigns it a direction (forward, left, right). Moves the player vehicle accordingly.
        if ((angles.y >=330 && angles.y<= 360) ||(angles.y >= 0 && angles.y <= 30))
        {
            controllerDirectionText.text = "Direction: Forward";

            // Vehicle doesn't move when controller points forwards.

        }
        else if((angles.y >= 180 && angles.y <= 330))
        {
            controllerDirectionText.text = "Direction: Left";

            // (x-)
            // Move our position a step closer to the target.            
            transform.position = Vector3.MoveTowards(transform.position, leftTarget.position, step);


        }
        else if ((angles.y >= 30 && angles.y <= 180))
        {
            controllerDirectionText.text = "Direction: Right";

            // (x+)
            // Move our position a step closer to the target.            
            transform.position = Vector3.MoveTowards(transform.position, rightTarget.position, step);


        }






    }
}
