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
    public pickupStuff pickupStuffScript;
    public objective objectiveScirpt;

    public Transform baseTransform;
    public Transform objCubeTransform;

    public GameObject objCube;

    float timer = 0;
    void Start(){
        targets = GetComponents<RayPerceptionSensorComponent3D>()[1];
        obstacles = GetComponents<RayPerceptionSensorComponent3D>()[0];
        movementScript = GetComponent<Movement>();
        pickupStuffScript = GetComponent<pickupStuff>();
        objectiveScirpt = mainObj.GetComponent<objective>();
        Debug.Log(targets.SensorName);
        Debug.Log(obstacles.SensorName);
    }
    public override void OnEpisodeBegin(){

        this.gameObject.transform.position = baseTransform.position;
        this.transform.rotation = baseTransform.rotation;

        Instantiate(objCube, objCubeTransform.position, Quaternion.identity);

        //objCube.transform.position = objCubeTransform.position;
        //objCube.transform.rotation = objCubeTransform.rotation;

        Transform mainOjbtransform = mainObj.transform;

        Destroy(mainObj);

        mainObj = Instantiate(MainObjprefab, mainOjbtransform.position, Quaternion.identity);

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        sensor.AddObservation(targets);
        sensor.AddObservation(obstacles);
        sensor.AddObservation(mainObj.transform.position);
        sensor.AddObservation(this.transform.rotation);
        sensor.AddObservation(pickupStuffScript.isSomethingPickedup);
        
    }

    public override void OnActionReceived(float[] vectorAction){

        if(vectorAction[0] < 0){
            movementScript.Rotate(1);
        }
        else if(vectorAction[0]> 0){
            movementScript.Rotate(-1);
        }

        if(vectorAction[1] < 0){
            movementScript.Walk(1);
        }
        else if(vectorAction[1]> 0){
            movementScript.Walk(-1);
        }

        if(vectorAction[2]> 0){
            bool pickup = pickupStuffScript.Pickup();

            if(pickup){
                AddReward(0.1f);
            }
            pickup = false;
        }

        if (objectiveScirpt.hasreturnedThing)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        timer += Time.deltaTime;

        if(timer > 60){
            AddReward(-0.2f);
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut){

    }

    



    
}
