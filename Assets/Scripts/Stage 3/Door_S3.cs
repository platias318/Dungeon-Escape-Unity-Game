using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;

public class Door_S3 : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private AudioSource sound;
    float direction = 1;
    bool isRotating = false;
    public bool openDoor = false;
    Vector3 v;
    Vector3 targetDirection;

    private void Update()
    {
        if ((openDoor) && !isRotating)
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

            }

            StartCoroutine(RotateDoor());

        }

    }

    public void Open()
    {
        openDoor = true;
    }

    public IEnumerator RotateDoor()
    {
        isRotating = true;
        if (gameObject.name == "DoorGate_Wooden_Right") direction = -1;

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
        openDoor = false;

    }
}
