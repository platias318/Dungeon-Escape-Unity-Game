using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PasscodeTrigger : MonoBehaviour
{

    [SerializeField]private Canvas canvasObj;
    [SerializeField]private Canvas KeyPadObj;
    private bool OpenKeyPad = false;
    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;

    void Start()
    {
        canvasObj.enabled = false;
        KeyPadObj.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
            if (!canvasObj.enabled) // when the player approaches the keypad, the text that he can enable it by pressing E is showing
            {
                canvasObj.enabled = true;
            }
    }
    private void OnTriggerStay(Collider other)
    {
        // Check if E is pressed and there's a delay since the last press
        if (Input.GetKey("e") && Time.time - lastEPressTime >= delayBetweenPresses)
        {

            lastEPressTime = Time.time;
            isEPressed = !isEPressed; // Toggle the flag

            if (isEPressed)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().lockCursor = false;
                Cursor.lockState = CursorLockMode.None;
                canvasObj.enabled = false;
                KeyPadObj.enabled = true;
                OpenKeyPad = true;
            }
            else
            {
                canvasObj.enabled = true;
                KeyPadObj.enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().lockCursor = true;
                Cursor.lockState = CursorLockMode.Locked;
                OpenKeyPad = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        {
            if (OpenKeyPad) // if he leaves the range and the keypad was open, close it
            {
                KeyPadObj.enabled = false;
            }
            if (canvasObj.enabled) // if he leaves the range and the Press E was visible, close it
            {
                canvasObj.enabled = false;
            }
        }
    }
}
