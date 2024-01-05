using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    [SerializeField] private Canvas canvasObj;
    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;       //keeps a delay between presses of the button E


    void Start()
    {
            canvasObj.enabled = false;
            gameObject.SetActive(true);
    }

    void Pickup()          //pick the item and pass it to the InventoryManager to handle it
    {
        InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
            InventoryManager.Instance.ListItems();
    }

    private void OnTriggerEnter(Collider other)
    {
            canvasObj.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        //check if E is pressed and there's a delay since the last press
        if (Input.GetKey("e") && Time.time - lastEPressTime >= delayBetweenPresses)
        {

            lastEPressTime = Time.time;
            isEPressed = !isEPressed; //toggle the flag

            if (isEPressed && InventoryManager.Instance.Items.Count < 4)    //if it fits in the inventory, pick it up
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
    private void OnTriggerExit(Collider other)
    {
        canvasObj.enabled = false;
    }
}




