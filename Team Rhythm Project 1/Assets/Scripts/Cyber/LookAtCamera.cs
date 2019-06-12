using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // What to look at.
    public Transform lookAtTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Point this at the camera.
        this.transform.LookAt(lookAtTarget);
    }
}
