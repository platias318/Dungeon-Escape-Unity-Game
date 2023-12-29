using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarelPickup : MonoBehaviour
{
    [SerializeField] private GameObject carriedPosition;
    [SerializeField] private Canvas pickupText;
    [SerializeField] private AudioSource audioEffect;
    [SerializeField] private GameObject dropPosition;
    private bool isEPressed = false; //if the key 'E' has been pressed so that the keypad appears
    private float lastEPressTime = 0f; //last frame the key 'E' was pressed
    private float delayBetweenPresses = 0.5f;
    static bool hasBarrel = false;
    private static GameObject barrel;

    void Start() // disable both canvas in the start
    {
        pickupText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerSkeleton")
        {
            if (!pickupText.enabled && !hasBarrel) // when the player approaches the barrel, the text that he can take it by pressing 'E' is showing
            {
                pickupText.enabled = true;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
            // Check if E is pressed and there's a delay since the last press
            if (Input.GetKey("e") && Time.time - lastEPressTime >= delayBetweenPresses)
            {
                lastEPressTime = Time.time;
                isEPressed = !isEPressed; // Toggle the flag

                if (isEPressed && !hasBarrel)
                {
                    Debug.Log("it was pressed and the barrel moved to the player");
                    barrel = gameObject;
                    hasBarrel = true;
                    gameObject.transform.position = carriedPosition.transform.position;
                    gameObject.transform.SetParent(carriedPosition.transform, true);
                    pickupText.enabled = false; //disable the 'press e to open the keypad' canvas element
                }else if (isEPressed && hasBarrel)
                {
                    Debug.Log("it was pressed but we have already a barrell, so we throw the old barrel");
                    barrel.transform.position = dropPosition.transform.position;
                    barrel.transform.SetParent(GameObject.FindGameObjectWithTag("Barrels").transform, true);
                    hasBarrel = false;
                    barrel = null;

                }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerSkeleton")
        {
            if (pickupText.enabled) // if he leaves the range and the Press E was visible, close it
            {
                pickupText.enabled = false;
            }
        }
    }


}
