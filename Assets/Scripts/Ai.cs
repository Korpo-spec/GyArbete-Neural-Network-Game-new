using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class Ai : Agent
{
    public RayPerceptionSensorComponent3D targets;

    void Start(){
        targets = GetComponent<RayPerceptionSensorComponent3D>();
    }
    public override void OnEpisodeBegin(){

        

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        
    }



    
}
