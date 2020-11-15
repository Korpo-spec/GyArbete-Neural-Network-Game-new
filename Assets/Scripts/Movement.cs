using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    public float movementSpeed = 10;
    public float rotationSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float movementX;
    float movementY;
    // Update is called once per frame
    void Update()
    {
        movementY = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
        movementX = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        

        
        //transform.Translate();
        
        
    }

    public void Rotate(int roatation){
        transform.Rotate(new Vector3(0, roatation, 0));
    }
    public void Walk(int directionn){
        Vector3 direction = transform.forward;
        direction.y = 0;
        direction.Normalize();
        transform.Translate(direction * directionn ,Space.World);
    }

    
}
