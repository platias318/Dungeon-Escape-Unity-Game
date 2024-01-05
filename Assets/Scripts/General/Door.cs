using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;

public class Door : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private AudioSource sound;
    public  float direction = 1;
    bool isRotating = false;
    public bool stageDoor;
    Vector3 v;
    Vector3 targetDirection;

    GameObject player;


    private void Update()
    {
        if (((triggered && Input.GetKeyDown(KeyCode.E)) || stageDoor) && !isRotating)
        {
            if (sound != null)
            {
                sound.Play();
            }
            if (isOpen) 
            {
                v = Vector3.down;

            }
            else
            {
                v = Vector3.up;
                //rotation relative to player position
                targetDirection = player.transform.InverseTransformPoint(transform.position);
                
                direction = Math.Sign(targetDirection.x);
            }
            
            StartCoroutine(RotateDoor());
            
        }
        
    }

    public IEnumerator RotateDoor()
    {
        
        isRotating = true;

        float elapsedTime = 0f;

        while (elapsedTime < rotationAngle / rotationSpeed)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(v * step * direction);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRotating = false;
        isOpen = !isOpen;
        stageDoor = false;


    }


    void OnTriggerEnter(Collider other)
    {
         player = other.gameObject;
         
         triggered = true;

    }

    private void OnTriggerExit(Collider other)
    {

         
         triggered = false;

    }
   
}
