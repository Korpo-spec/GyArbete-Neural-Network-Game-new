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
                foreach(Transform child in transform){
                    child.transform.parent = theMap.transform;
                    child.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                timer = 0;
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
}
