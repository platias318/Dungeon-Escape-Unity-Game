using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Door3 : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private AudioSource sound;
    [SerializeField] private Passcode passcode; // Reference to the Passcode script
    [SerializeField] private PressurePlate2 pressurePlate; // Reference to the PressurePlate2 script
    [SerializeField] private Canvas canvasObj;
    [SerializeField] private AudioSource unlockAudioEffect;
    float direction = 1;
    bool isRotating = false;
    Vector3 v;
    Vector3 targetDirection;

    GameObject player;
    private bool hasPlayed = false;


    private void Update()
    {
        if (passcode.isCorrect() && pressurePlate.getEnabled())
        {
            if (!unlockAudioEffect.isPlaying && !hasPlayed)
            {
                hasPlayed = true;
                unlockAudioEffect.Play();
            }
        }
            if (triggered && Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            // Check if the keycode is correct and the pressure plate is enabled
            if (passcode.isCorrect() && pressurePlate.getEnabled())
            {
                Debug.Log("door enabled");
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
                    targetDirection = player.transform.InverseTransformPoint(transform.position);
                    Debug.Log(targetDirection);
                    direction = Math.Sign(targetDirection.x);
                }

                StartCoroutine(RotateDoor());
                canvasObj.enabled = true; // Enable the canvas
            }
            else
            {
                Debug.Log("Door remains closed");
            }
        }
    }

    IEnumerator RotateDoor()
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
        canvasObj.enabled = false; // Disable the canvas after the door is opened

    }


    void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
        Debug.Log("trigger enter");
        triggered = true;

        canvasObj.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("trigger exit");
        triggered = false;
        canvasObj.enabled = false;
    }

}
