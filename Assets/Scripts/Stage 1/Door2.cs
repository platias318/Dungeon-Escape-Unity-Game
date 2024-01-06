using System.Collections;
using UnityEngine;
using System;

public class Door2 : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private AudioSource sound;
    [SerializeField] private Canvas canvasObj; // The canvas that will be enabled
    float direction = 1;
    bool isRotating = false;
    Vector3 v;
    Vector3 targetDirection;

    GameObject player;
    Item key;

    private void Update()
    {
        if (triggered && Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            key = InventoryManager.Instance.getItem("Key");
            if (key != null) // If the user has the key
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
                    targetDirection = player.transform.InverseTransformPoint(transform.position);
                    Debug.Log(targetDirection);
                    direction = Math.Sign(targetDirection.x);
                }

                StartCoroutine(RotateDoor());
                InventoryManager.Instance.UseItem(key); // Use the key
                canvasObj.enabled = true; // Enable the canvas
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
        triggered = true;

        key = InventoryManager.Instance.getItem("Key");
        if (key != null) // If the user has the key
        {
            canvasObj.enabled = true; // Enable the canvas
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        canvasObj.enabled = false; // Disable the canvas when the user leaves the trigger area
    }
}