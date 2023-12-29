using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerScript : MonoBehaviour
{
    public GameObject plate;

    public GameObject[] notes;

    public Material newMaterial;
    private Material originalMaterial;



    void Start()
    {
   
        originalMaterial = GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void OnTriggerEnter(Collision other)
    {

        GetComponent<Renderer>().material = newMaterial;

    }

    void OnTriggerExit(Collision other)
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}
