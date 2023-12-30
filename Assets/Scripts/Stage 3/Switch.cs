using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;
    private bool isEnabled = false;
    [SerializeField] private AudioSource switchSoundEffect;

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