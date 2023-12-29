using UnityEngine;

public class PasscodeTrigger : MonoBehaviour
{

    [SerializeField]private Canvas canvasObj; //the canvas that appears when the user is near the keypad and can press 'E' to use it
    [SerializeField]private Canvas KeyPadObj;//the keypad canvas object
    private bool OpenKeyPad = false; //if the keypad canvas is opened in the current frame
    private bool isEPressed = false; //if the key 'E' has been pressed so that the keypad appears
    private float lastEPressTime = 0f; //last frame the key 'E' was pressed
    private float delayBetweenPresses = 0.5f;

    void Start() // disable both canvas in the start
    {
        canvasObj.enabled = false;
        KeyPadObj.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
            if (!canvasObj.enabled) // when the player approaches the keypad, the text that he can enable it by pressing 'E' is showing
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
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().lockCursor = false;//lock cursor so that it doesnt move the player when we move the mouse
                Cursor.lockState = CursorLockMode.None;
                canvasObj.enabled = false; //disable the 'press e to open the keypad' canvas element
                KeyPadObj.enabled = true; // enable the keypad canvas sto that the user can interact with it
                OpenKeyPad = true;
            }
            else
            {
                canvasObj.enabled = true;
                KeyPadObj.enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().lockCursor = true;
                Cursor.lockState = CursorLockMode.Locked;
                OpenKeyPad = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        {
            if (OpenKeyPad) // if he leaves the range and the keypad was open, close it
            {
                KeyPadObj.enabled = false;
            }
            if (canvasObj.enabled) // if he leaves the range and the Press E was visible, close it
            {
                canvasObj.enabled = false;
            }
        }
    }
}
