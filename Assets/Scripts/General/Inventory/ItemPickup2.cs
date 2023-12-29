using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup2 : MonoBehaviour
{
    public Item Item;
    private Canvas canvasObj;
    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;


    void Start()
    {
        canvasObj = GameObject.FindGameObjectWithTag("PickItem").GetComponent<Canvas>();
        canvasObj.enabled = false;
        gameObject.SetActive(true);
    }
    public void Pickup()
    {
        // Disable the pressure plate before destroying the game object
        PressurePlate2 pressurePlate = FindObjectOfType<PressurePlate2>();
        // Check if the barrel being picked up is the one on the pressure plate
        if (pressurePlate != null && Vector3.Distance(transform.position, pressurePlate.transform.position) < 3.5f)
        {
            // If it is, disable the pressure plate
            pressurePlate.DisablePressurePlate();
        }

        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        InventoryManager.Instance.ListItems();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerSkeleton")
        {
            canvasObj.enabled = true;
            Debug.Log(other.name);
        }
    }
    private void Update()
    {
        // Check if D is pressed and there's a delay since the last press
        if (Input.GetKey("f") && Time.time - lastEPressTime >= delayBetweenPresses)
        {
            lastEPressTime = Time.time;

            // If the user has a barrel in their inventory
            if (InventoryManager.Instance.getItem("Barrel") != null)
            {
                // Calculate a position that's 2 units in front of the player
                Vector3 dropPosition = GameObject.FindGameObjectWithTag("DropItemPosition").transform.position;

                // Create a new GameObject to represent the drop position
                GameObject dropPositionObj = new GameObject();
                dropPositionObj.transform.position = dropPosition;

                // Call the DropItem method
                InventoryManager.Instance.DropItem(InventoryManager.Instance.getItem("Barrel"), dropPositionObj);

                // Destroy the dropPositionObj as it's no longer needed
                Destroy(dropPositionObj);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerSkeleton")
        {
            // Check if E is pressed and there's a delay since the last press
            if (Input.GetKey("e") && Time.time - lastEPressTime >= delayBetweenPresses)
            {
                lastEPressTime = Time.time;
                isEPressed = !isEPressed; // Toggle the flag

                // If the user doesn't have a barrel in their inventory and there's room for more items
                if (isEPressed && InventoryManager.Instance.Items.Count < 4 && InventoryManager.Instance.getItem("Barrel") == null)
                {
                    canvasObj.enabled = false;
                    gameObject.SetActive(false);
                    Pickup();
                }
                else
                {
                    canvasObj.enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerSkeleton")
        {
            canvasObj.enabled = false;
        }
    }
}




