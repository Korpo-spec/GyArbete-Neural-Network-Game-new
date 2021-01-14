using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HeatMap : MonoBehaviour
{   
    public Ai scriptAI;
    private long[,] movementTrack = new long[100,100];
    private long[,] DeathTrack = new long[100,100];
    private float frameTracker;

    void Start(){
        for (int i = 0; i < DeathTrack.GetLength(0); i++)
        {
            for (int x = 0; x < DeathTrack.GetLength(0); x++)
            {
                DeathTrack[i,x] = 0;
            }
        }
    }
    void LateUpdate(){
        frameTracker++;
        movementTrack[Mathf.RoundToInt(transform.position.x* 1), Mathf.RoundToInt(transform.position.z* 1)] += 1;

        if(scriptAI.wallcollision){
            DeathTrack[Mathf.RoundToInt(transform.position.x* 1), Mathf.RoundToInt(transform.position.z* 1)] += 1;

        }

        if(Input.GetKeyDown(KeyCode.Keypad3)){
            string[] fileToSave = new string[100];
            long[] joinedarray = new long[100];

            for (int i = 0; i < movementTrack.GetLength(0); i++)
            {   
                
                int amountOfTimesRun = 0;
                
                for (int y = 0; y < 100; y++)
                {
                    amountOfTimesRun++;
                    Debug.Log(amountOfTimesRun);
                    joinedarray[y] = movementTrack[i,y];
                }
                
                fileToSave[i] = string.Join(":",joinedarray);
            }
            
            
            File.WriteAllLines(@"D:\Github\MineSweeper\GyArbete-Neural-Network-Game-new\hejsan.txt", fileToSave);

            for (int i = 0; i < movementTrack.GetLength(0); i++)
            {   
                
                int amountOfTimesRun = 0;
                
                for (int y = 0; y < 100; y++)
                {
                    amountOfTimesRun++;
                    Debug.Log(amountOfTimesRun);
                    joinedarray[y] = DeathTrack[i,y];
                }
                
                fileToSave[i] = string.Join(":",joinedarray);
            }
            File.WriteAllLines(@"D:\Github\MineSweeper\GyArbete-Neural-Network-Game-new\hejsanusch.txt", fileToSave);
        }

    }
    
}
