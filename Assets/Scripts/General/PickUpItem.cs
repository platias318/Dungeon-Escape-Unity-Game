using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Canvas canvasObj;
    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        canvasObj.enabled = false;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                canvasObj.enabled = false;
                gameObject.SetActive(false);

            }
            else
            {
                canvasObj.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    { }

}
