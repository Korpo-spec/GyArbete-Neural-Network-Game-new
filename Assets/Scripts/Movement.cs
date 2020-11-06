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

    // Update is called once per frame
    void Update()
    {
        float movementY = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
        float movementX = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        transform.Rotate(new Vector3(0, movementY, 0));

        Vector3 direction = transform.forward;
        direction.y = 0;
        direction.Normalize();
        transform.Translate(direction * movementX ,Space.World);
        //transform.Translate();
        
        
    }

    
}
