using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] float lowerOffset = 0.0f; //adjust the offset for the lower position of the broken object
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

    private void OnTriggerStay(Collider other)        //if the player is in the correct area 
    {
        hammer = InventoryManager.Instance.getItem("Hammer");     
        if (hammer != null && isEPressed && !destroyed)        //if the player has the hammer item in his inventory and the object is not destroyed
        {
            if (sound != null)
            {
                sound.Play();
            }
            HideCanvas();       //hide the message
            //move the current breakable object to a new position and rotation
            Vector3 newPosition = transform.position - new Vector3(0f, lowerOffset, 0f);           //basically rotate the object in order for it to appear broken down
            Quaternion newRotation = Quaternion.Euler(270.0f, 0f, 0f);
            transform.SetPositionAndRotation(newPosition, newRotation);
            MeshCollider meshCollider = GetComponent<MeshCollider>();
            Destroy(meshCollider);               //destroy its mesh collider so the player can pass
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