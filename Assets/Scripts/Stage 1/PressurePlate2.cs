using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate2 : MonoBehaviour
{
    [SerializeField] private GameObject pressurePlateRed;
    [SerializeField] private GameObject pressurePlateGreen;
    private bool isEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        //the red coloured pressure plate is visible , and the green one invisible
        pressurePlateRed.SetActive(true);
        pressurePlateGreen.SetActive(false);

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (!isEnabled) { //if the pressure plate was not enabled, enable it because the user or an object is standing on it
            pressurePlateRed.SetActive(false);
            pressurePlateGreen.SetActive(true);
            isEnabled = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (isEnabled) //if the user or an object is leaving the pressure plate , disable it and turn it into red       
        {
            pressurePlateRed.SetActive(true);
            pressurePlateGreen.SetActive(false);
            isEnabled = false;
        }
    }

    public bool getEnabled() //returns whether the pressure plate is enabled in the current frame
    {
        return isEnabled;
    }


}
