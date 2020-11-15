using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupStuff : MonoBehaviour
{

    GameObject theMap;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        theMap = GameObject.Find("Map");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")&&timer > 0.2f){
            if(transform.childCount > 0){
                
            }
        }   
        timer += Time.deltaTime;
    }


    private void OnTriggerStay(Collider other) {
        
        if (Input.GetKeyDown("space")&&timer > 0.2f && other.tag == "pickup")
        {   
            
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            

            timer = 0;
           
            other.transform.parent = this.transform;
            
            
        }

    }
    public bool isCollidingWithCube;
    public GameObject collidingObj;
    private void OnTriggerEnter(Collider other){
        if(other.tag == "pickup"){
            isCollidingWithCube = true;
            collidingObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider Other){
        collidingObj = null;
        isCollidingWithCube = false;
    }

    public bool isSomethingPickedup;
    public bool Pickup(){
        if(timer> 0.2f && isCollidingWithCube && !isSomethingPickedup){
            collidingObj.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            

            timer = 0;
           
            collidingObj.transform.parent = this.transform;
            isSomethingPickedup = true;
            if(collidingObj.name == "obj cube"){
                collidingObj.name = "pickedup obj cube";
                return true; 
            }
            
        }
        else if(isSomethingPickedup && timer > 0.2f){

            foreach(Transform child in transform){
                child.transform.parent = theMap.transform;
                child.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
            timer = 0;

            isSomethingPickedup = false;
            
        }
        return false;
    }
}
