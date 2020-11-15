using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objective : MonoBehaviour
{
    public GameObject[] motorThings;
    public Material targetMaterial;
    int amountOfPartsFound = 0;

    public bool hasreturnedThing;
    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "pickup")
        {
            Destroy(other.gameObject);
            MeshRenderer motorMesh = motorThings[amountOfPartsFound].GetComponent<MeshRenderer>();

            motorMesh.material = targetMaterial;
            amountOfPartsFound++;
            hasreturnedThing = true;
        }
    }
}
