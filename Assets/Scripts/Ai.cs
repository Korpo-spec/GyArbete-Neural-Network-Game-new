using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class Ai : Agent
{
    public RayPerceptionSensorComponent3D targets;
    public RayPerceptionSensorComponent3D obstacles;
    public GameObject mainObj;
    public GameObject MainObjprefab;
    public Movement movementScript;
    public GameObject pickupStuffScript;
    public objective objectiveScirpt;

    public Transform baseTransform;
    public Transform objCubeTransform;

    public GameObject objCube;

    public GameObject pickupObj;
    float timer = 0;
    public override void Initialize(){
        targets = GetComponents<RayPerceptionSensorComponent3D>()[1];
        obstacles = GetComponents<RayPerceptionSensorComponent3D>()[0];
        movementScript = GetComponent<Movement>();
        
        objectiveScirpt = mainObj.GetComponent<objective>();
        Debug.Log(targets.SensorName);
        Debug.Log(obstacles.SensorName);
    }
    public override void OnEpisodeBegin(){

        this.gameObject.transform.position = baseTransform.position;
        this.transform.rotation = baseTransform.rotation;
        
        if(GameObject.Find("pickedup obj cube")){
            Destroy(GameObject.Find("pickedup obj cube"));
            pickupStuffScript.GetComponent<pickupStuff>().collidingObj = null;
            pickupStuffScript.GetComponent<pickupStuff>().isCollidingWithCube = false;
        }
        if(!GameObject.Find("obj cube")){

            
            pickupObj = Instantiate(objCube, objCubeTransform.position, Quaternion.identity);
            pickupObj.name = "obj cube";
            
            Debug.Log(pickupObj);
            


            pickupObj.transform.parent = pickupStuffScript.GetComponent<pickupStuff>().theMap.transform;
            //pickupObj.transform.SetParent(pickupStuffScript.GetComponent<pickupStuff>().theMap.transform, false);
        }
        
        pickupStuffScript.GetComponent<pickupStuff>().isSomethingPickedup = false;
        pickupStuffScript.GetComponent<pickupStuff>().objectInHand.GetComponent<MeshRenderer>().material = pickupStuffScript.GetComponent<pickupStuff>().nothingMaterial;
        pickupStuffScript.GetComponent<pickupStuff>().objectInHand.GetComponent<BoxCollider>().isTrigger = true;

        originalDistance = Vector3.Distance(mainObj.transform.position, pickupObj.transform.position);
        
        //objCube.transform.position = objCubeTransform.position;
        //objCube.transform.rotation = objCubeTransform.rotation;

        Transform mainOjbtransform = mainObj.transform;

        Destroy(mainObj);

        mainObj = Instantiate(MainObjprefab, mainOjbtransform.position, Quaternion.identity);

    }
    float originalDistance;
    public override void CollectObservations(VectorSensor sensor)
    {
        
        //sensor.AddObservation(targets);
        //sensor.AddObservation(obstacles);
        sensor.AddObservation(mainObj.transform.position);
        sensor.AddObservation(this.transform.rotation.y);
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(pickupStuffScript.GetComponent<pickupStuff>().isSomethingPickedup);
        
    }

    
    public override void OnActionReceived(float[] vectorAction){

        if(this.transform.position.y < 11){
            SetReward(-0.4f);
            Debug.Log("fell off");
            EndEpisode();
        }
        //Debug.Log(vectorAction[0]-1);
        //Debug.Log(vectorAction[1]);
        //Debug.Log(vectorAction[2]);
        if(vectorAction[0]-1 < 0){
            movementScript.Rotate(1);
        }
        else if(vectorAction[0]-1 > 0){
            movementScript.Rotate(-1);
        }

        if(vectorAction[1] < 0){
            movementScript.Walk(-1);
        }
        else if(vectorAction[1] > 0){
            movementScript.Walk(1);
        }

        if(vectorAction[2] == 1){
            
            bool pickup = pickupStuffScript.GetComponent<pickupStuff>().Pickup();
            //Debug.Log("ran");
            if(pickup){
                AddReward(0.5f);
                Debug.Log("added reward");
            }
            pickup = false;
            
        }

        if (mainObj.GetComponent<objective>().hasreturnedThing)
        {
            SetReward(3.0f);
            AddReward((timer/30));
            Debug.Log("has returned");
            EndEpisode();
        }

        if (wallcollision)
        {
            Debug.Log("Wall hit");
            AddReward(-1f);
            EndEpisode();
        }

        

        timer += Time.deltaTime;

        if(timer > 30){
            float dist = Vector3.Distance(mainObj.transform.position, pickupObj.transform.position);
            if(originalDistance < dist){
                SetReward(-0.5f);
                Debug.Log("further away");
            }
            else{
                SetReward(1f - dist/originalDistance);
                Debug.Log(1f - dist/originalDistance);
            }
            
            timer = 0;
            Debug.Log("timer ending");
            EndEpisode();
        }

        
    }

    public override void Heuristic(float[] actionsOut){
        actionsOut[0] = Input.GetAxisRaw("Horizontal") + 1;
        actionsOut[1] = Input.GetAxisRaw("Vertical");
        
        if(Input.GetKey(KeyCode.Space)){
            actionsOut[2] = 1;
        }
        else{
            actionsOut[2] = 0;
        }
    }

    public bool wallcollision;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Wall")
        {
            wallcollision = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Wall")
        {
            wallcollision = false;
        }
    }
    



    
}
