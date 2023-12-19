using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightRangeChange : MonoBehaviour
{
    private Light torchLight;
    [SerializeField] private float minIntensity = 0.4f;
    [SerializeField]private float maxIntensity= 2.7f;
    [SerializeField]private float oscillationSpeed = 2.0f;      // Speed of the oscillation
    void Start()
    {
     //   GameObject GameObject = gameObject;
        torchLight = GetComponent<Light>(); 
    }

    private void Update()
    {
        if (torchLight != null)
        {
            // Calculate the oscillation factor based on time and speed
            float oscillationFactor = Mathf.Sin(Time.time * oscillationSpeed);

            // Map the oscillation factor to a range between 0 and 1
            float t = Mathf.InverseLerp(-1.0f, 1.0f, oscillationFactor);

            // Use Mathf.Lerp to smoothly interpolate between minIntensity and maxIntensity
            float newIntensity = Mathf.Lerp(minIntensity, maxIntensity, t);

            // Apply the new intensity to the light
            torchLight.intensity = newIntensity;
        }
    }

}
