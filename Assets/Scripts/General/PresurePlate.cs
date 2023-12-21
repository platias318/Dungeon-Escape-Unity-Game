using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Rendering;
using UnityEngine;

public class PresurePlate : MonoBehaviour
{
    public Vector3 originalPos;
    [SerializeField] public float sinkAmount;
    private float sunkAmount = 0.0f;
    private float s;
    bool moveBack = false;

    public Material newMaterial;
    private Material originalMaterial;
    [SerializeField] private float downSpeed = 0.01f;
    [SerializeField] private float upSpeed = 0.1f;
    [SerializeField] private string objectTag;

    float time;
    private bool sink = false;


    private void Start()
    {
        originalPos = transform.position;
        originalMaterial = GetComponent<Renderer>().material;
    

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == objectTag)
        {
            collision.transform.parent = transform;
            GetComponent<Renderer>().material = newMaterial;
            sink = true;
            moveBack = false;
            
            

        }
    }

    void Update()
    {   
       
        if (sink && (sunkAmount < sinkAmount))
        {
           s = downSpeed * Time.deltaTime;
           Debug.Log(transform.position.y);
           transform.Translate(Vector3.down * s);
           sunkAmount += s;

        }
        if (moveBack && sunkAmount > 0)
        {
            s = upSpeed * Time.deltaTime;
            Debug.Log(transform.position.y);
            transform.Translate(Vector3.up * s);
            sunkAmount -= s;
        }
       
    }
   
        
            
   
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == objectTag)
        {
            collision.transform.parent = null;
            GetComponent<Renderer>().material = originalMaterial;
            sink = false;
            moveBack = true;

        }
    }
}
