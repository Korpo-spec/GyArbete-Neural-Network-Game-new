using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool isGamer;
    public void IsGamer(){
        isGamer = true;
    }

    public void IsNotGamer(){
        isGamer = false;
        FileStream file = File.Open(@"Results.csv", FileMode.OpenOrCreate);
        file.Close();
        string[]  contents = File.ReadAllLines(@"Results.csv");
    }
}
