using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{

    [SerializeField] private Canvas canvasObj;
    [SerializeField] private Image image;          //image of the note
    [SerializeField] private TextMeshProUGUI text;

    private bool open = false;
    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;

    void Start()
    {
        canvasObj.enabled = false; // the message that we can press the lever is disabled at the start 
        image.enabled = false;
    }

    void Update()
    {
        isEPressed = Input.GetKey(KeyCode.E);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        ShowCanvas();   //shows the note in the screen
    }

    private void OnTriggerStay(Collider other)
    {
        if (isEPressed && Time.time - lastEPressTime >= delayBetweenPresses)          //same principle as Switch script
        {
            lastEPressTime = Time.time;
            if (open)                           //if the note is closed open it and vice versa
            {
                open = false;
                image.enabled = false;
                Debug.Log("image disabled");
                text.enabled = true;
            }
            else
            {
                open = true;
                image.enabled = true;
                Debug.Log("image enabled");
                text.enabled = false;
            }
            isEPressed = false;
        }
    }

    private void OnTriggerExit(Collider other)        //on exit close the note if the player hasnt closed it
    {
        Debug.Log("Left");
        HideCanvas();
        text.enabled = true;
        image.enabled = false;
        open = false;
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

