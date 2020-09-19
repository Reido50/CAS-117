using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("up"))
        {
            if(transform.position.z < -1)
            {
                transform.position = transform.position + new Vector3(0,0,1);
            }
        }
        else if(Input.GetKeyUp("down"))
        {
            if (transform.position.z > -10)
            {
                transform.position = transform.position + new Vector3(0, 0, -1);
            }
        }
    }
}
