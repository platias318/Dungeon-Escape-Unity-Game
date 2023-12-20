using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool middle = true;
    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;

    void Update()
    {
        isEPressed = Input.GetKey(KeyCode.E);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
    }

    private void OnTriggerStay(Collider other)
    {
        if (isEPressed && Time.time - lastEPressTime >= delayBetweenPresses)
        {
            lastEPressTime = Time.time;
            if (middle)
            {
                transform.Rotate(new Vector3(45f, 0f, 0f), Space.Self);
                middle = false;
            }
            else
            {
                transform.Rotate(new Vector3(-45f, 0f, 0f), Space.Self);
                middle = true;
            }

            isEPressed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left");
    }
}