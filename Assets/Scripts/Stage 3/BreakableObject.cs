using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] float lowerOffset = 0.0f; // Adjust the offset for the lower position of the broken object
    [SerializeField] private Canvas canvasObj;
    [SerializeField] private AudioSource sound;
    Item hammer;
    private bool isEPressed = false;
    private bool destroyed = false;


    void Start()
    {
        canvasObj.enabled = false; 
    }

    void Update()
    {
        isEPressed = Input.GetKey(KeyCode.E);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!destroyed) { ShowCanvas(); }
    }

    private void OnTriggerStay(Collider other)
    {
        hammer = InventoryManager.Instance.getItem("Hammer");
        if (hammer != null && isEPressed && !destroyed)
        {
            if (sound != null)
            {
                sound.Play();
            }
            HideCanvas();
            // Move the current breakable object to a new position and rotation
            Vector3 newPosition = transform.position - new Vector3(0f, lowerOffset, 0f);
            Quaternion newRotation = Quaternion.Euler(270.0f, 0f, 0f);
            transform.SetPositionAndRotation(newPosition, newRotation);
            MeshCollider meshCollider = GetComponent<MeshCollider>();
            Destroy(meshCollider);
            destroyed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HideCanvas();
    }

    private void ShowCanvas()
    {
        if (!canvasObj.enabled)
        {
            canvasObj.enabled = true;
        }
    }

    private void HideCanvas()
    {
        if (canvasObj.enabled)
        {
            canvasObj.enabled = false;
        }
    }
}