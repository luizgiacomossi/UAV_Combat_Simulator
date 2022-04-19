using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceController : MonoBehaviour
{
    private Rigidbody referenceRb;
    public float sizeStep ;
    // Start is called before the first frame update
    void Start()
    {
        referenceRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        // arrow up
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.position += new Vector3(0, 0, sizeStep);
        }
        // arrow down
        if(Input.GetKey(KeyCode.DownArrow)){
            transform.position += new Vector3(0, 0, -sizeStep);
        }

        // arrow left

        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.position += new Vector3(-sizeStep, 0, 0);
        }

        // arrow right

        if(Input.GetKey(KeyCode.RightArrow)){
            transform.position += new Vector3(sizeStep, 0, 0);
        }
        // B pressed
        if(Input.GetKeyDown(KeyCode.B)){
            transform.position += new Vector3(0, -sizeStep, 0);
        }
        // C pressed
        if(Input.GetKeyDown(KeyCode.C)){
            transform.position += new Vector3(0, sizeStep, 0);
        }

    }
}
