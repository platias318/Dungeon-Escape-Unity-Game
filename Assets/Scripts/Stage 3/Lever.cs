using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool middle = true;            //lever is in the middle/disabled
    private bool isEPressed = false;        //same principle as switch script
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;
    [SerializeField] private Canvas canvasObj;
    [SerializeField] private AudioSource leverSoundEffect;

    private static bool leverA = false;       //we keep all these static variables because we want the gate to open when the correct levers are enabled
    private static bool leverB = false;
    private static bool leverC = false;
    private static bool leverD = false;
    private static bool leverE = false;
    private static bool leverF = false;
    private static bool open = false;

    [SerializeField] private GameObject door1;   //doors we want to open
    [SerializeField] private GameObject door2;

    void Start()
    {
        canvasObj.enabled = false; // the message that we can press the lever is disabled at the start
    }

    void Update()
    {
        isEPressed = Input.GetKey(KeyCode.E);
    }

    private void OnTriggerEnter(Collider other)
    {
        ShowCanvas(); // show the text that the user is elligible to move the lever
    }

    private void OnTriggerStay(Collider other)
    {
        if (isEPressed && Time.time - lastEPressTime >= delayBetweenPresses)         //same principle as switch script
        {
            if (leverSoundEffect != null)
            {
                leverSoundEffect.Play();
            }

            lastEPressTime = Time.time;
            if (middle)                              //if in middle rotate downwards and vice versa
            {
                transform.Rotate(new Vector3(45f, 0f, 0f), Space.Self);
                middle = false;
            }
            else
            {
                transform.Rotate(new Vector3(-45f, 0f, 0f), Space.Self);
                middle = true;
            }
            UpdateLever();

            isEPressed = false;
        }
    }

    private void UpdateLever()
    {
        if (gameObject.name == "LeverA") leverA = !leverA;        //update the correct lever according to game object name
        if (gameObject.name == "LeverB") leverB = !leverB;
        if (gameObject.name == "LeverC") leverC = !leverC;
        if (gameObject.name == "LeverD") leverD = !leverD;
        if (gameObject.name == "LeverE") leverE = !leverE;
        if (gameObject.name == "LeverF") leverF = !leverF;
        CheckCondition();                                     //check if the correct levers are enabled
    }

    private void CheckCondition()
    {
        if (!leverA && leverB && !leverC && leverD && leverE && !leverF)          //open the door if they are
        {
            Debug.Log("levers are good");
            door1.gameObject.GetComponent<Door_S3>().Open();                //changes the open variable in the doors in order for them to open
            door2.gameObject.GetComponent<Door_S3>().Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left");
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