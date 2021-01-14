using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupStuff : MonoBehaviour
{

    public GameObject theMap;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        theMap = GameObject.Find("Map");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("space")&&timer > 0.2f){
            if(transform.childCount > 0){
                
            }
        }   */
        timer += Time.deltaTime;
    }


    private void OnTriggerStay(Collider other) {
        /*
        if (Input.GetKeyDown("space")&&timer > 0.2f && other.tag == "pickup")
        {   
            
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            

            timer = 0;
           
            other.transform.parent = this.transform;
            
            
        }
        */

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

    public GameObject objectInHand;
    public Material targetMaterial;
    public Material nothingMaterial;
    public bool Pickup(){
        if(timer> 0.3f && isCollidingWithCube && !isSomethingPickedup && collidingObj != null){

            //collidingObj.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            Destroy(collidingObj);

            timer = 0;
            objectInHand.GetComponent<MeshRenderer>().material = targetMaterial;
            objectInHand.GetComponent<BoxCollider>().isTrigger = false;
            
            gameObject.tag="pickup";
            

            isSomethingPickedup = true;
            
            /*
            Vector3 originalScale = collidingObj.transform.localScale;
           
            collidingObj.transform.parent = this.transform;
            collidingObj.transform.localScale = originalScale;
            isSomethingPickedup = true;
            
            collidingObj.name = "pickedup obj cube";
            */
            if(collidingObj.name == "obj cube"){
                return true; 
            }
            
            
            
        }
        else if(isSomethingPickedup && timer > 0.3f){
            /*
            foreach(Transform child in transform){
                Vector3 originalScale = child.transform.localScale;
                child.transform.parent = theMap.transform;
                child.transform.localScale = originalScale;
                child.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
            */
            GameObject newObj =Instantiate(objectInHand, objectInHand.transform);
            
            newObj.transform.parent = theMap.transform;
            newObj.tag = "pickup";
            newObj.gameObject.name = "pickedup obj cube";
            
            newObj.transform.localScale =new Vector3(3.2488f,3.2488f,3.2488f);
            newObj.transform.Translate(new Vector3(0,-2f, 0));
            objectInHand.GetComponent<MeshRenderer>().material = nothingMaterial;
            objectInHand.GetComponent<BoxCollider>().isTrigger = true;
            
            gameObject.tag="no";
            
            timer = 0;

            isSomethingPickedup = false;
            
        }
        return false;
    }
}
