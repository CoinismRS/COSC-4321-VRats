using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelInput : MonoBehaviour
{

    public static GameObject currentObject;
    int currentID;
    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Send out a Raycast and returns an array filled with everything it hit
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        //Goes through all the hit objects and checks if any of them were out button
        for (int i = 0; i < hits.Length;  i++)
        {
            RaycastHit hit = hits[i];

            //Use the object ID to determine if I have already run the code for this object
            int id = hit.collider.gameObject.GetInstanceID();

            if (currentID != id)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;

                //Checks based off the name
                string name = currentObject.name;
                if (name == "Next")
                {
                    Debug.Log("HIT NEXT");
                }

                //Checks based off the tag
                string tag = currentObject.tag;
                if(tag == "Button")
                {
                    Debug.Log("HIT BUTTON");
                }
            }
        }
    }
}
