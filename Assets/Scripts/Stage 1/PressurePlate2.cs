using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate2 : MonoBehaviour
{
    [SerializeField] private GameObject pressurePlateRed;
    [SerializeField] private GameObject pressurePlateGreen;
    private bool enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        pressurePlateRed.SetActive(true);
        pressurePlateGreen.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) {
            pressurePlateRed.SetActive(false);
            pressurePlateGreen.SetActive(true);
            enabled = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {
        if (enabled)        
        {
            pressurePlateRed.SetActive(true);
            pressurePlateGreen.SetActive(false);
            enabled = false;
        }
    }

    public bool getEnabled()
    {
        return enabled;
    }


}
