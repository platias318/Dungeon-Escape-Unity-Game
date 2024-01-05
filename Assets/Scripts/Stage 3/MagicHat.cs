using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHat : MonoBehaviour
{
    private bool isEPressed = false;          //same principle as switch script
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;
    [SerializeField] private GameObject symbol1;
    [SerializeField] private GameObject symbol2;
    [SerializeField] private GameObject symbol3;

    private MeshRenderer meshRenderer1;
    private MeshRenderer meshRenderer2;
    private MeshRenderer meshRenderer3;

    private void Start()        //get the mesh renderer of our symbols
    {
        meshRenderer1 = symbol1.GetComponent<MeshRenderer>();
        meshRenderer2 = symbol2.GetComponent<MeshRenderer>();
        meshRenderer3 = symbol3.GetComponent<MeshRenderer>();

    }

    private void OnTriggerStay(Collider other)
    {
        // Check if E is pressed and there's a delay since the last press
        if (Input.GetKey("e") && Time.time - lastEPressTime >= delayBetweenPresses)
        {

            lastEPressTime = Time.time;
            isEPressed = !isEPressed; // Toggle the flag
            meshRenderer1.enabled = true;  //when the user wears the hat we want the symbols to appear, so we enable their mesh renderer (initially it is disabled)
            meshRenderer2.enabled = true;
            meshRenderer3.enabled = true;
        }
    }
}
