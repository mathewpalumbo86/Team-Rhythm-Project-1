using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    public GameObject floor;
    public Transform spawn;

    Vector3 startLoc;

    public float tX,tY,tZ;
    




    // Start is called before the first frame update
    void Start()
    {



        
        startLoc = new Vector3(floor.transform.position.x, floor.transform.position.y, floor.transform.position.y);





    }

    // Update is called once per frame
    void Update()
    {

        //if()
        transform.Translate(tX, tY, tZ);// moving the floor 







    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ResetZone")
        {



            floor.transform.position = startLoc; 



        }






    }

    



}
