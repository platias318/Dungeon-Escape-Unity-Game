using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool isEPressed = false;          //Check condition of switch
    private float lastEPressTime = 0f;          //The following 2 variables are used in order to prevent the user from pressing E multiple times at once
    private float delayBetweenPresses = 0.5f;
    private bool isEnabled = false;             
    [SerializeField] private AudioSource switchSoundEffect;     //audio effect

    void Update()           //checks if the user pressed the e button (interaction)
    {
        isEPressed = Input.GetKey(KeyCode.E);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
    }

    private void OnTriggerStay(Collider other)
    {
        if (isEPressed && Time.time - lastEPressTime >= delayBetweenPresses)         //if the button is pressed and a certain amount of time passed
        {
            if (switchSoundEffect != null)
            {
                switchSoundEffect.Play();
            }
            lastEPressTime = Time.time;
            isEnabled = true;
            isEPressed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left");
    }

    public bool IsEnabled() { return isEnabled; }
}