using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2 : MonoBehaviour
{
    [SerializeField] private GameObject lever;
    [SerializeField] private Canvas canvasObj;
    [SerializeField] private AudioSource leverSoundEffect; // the audio effect when a lever is moved
    public LeverChecker leverChecker;

    private bool isEPressed = false;
    private float lastEPressTime = 0f;
    private float delayBetweenPresses = 0.5f;
    private bool middle = true;

    //static counters so that we keep track the miber of times a lever is interacted with
    private static int counterR = 0;
    private static int counterG = 0;
    private static int counterO = 0;

    void Start()
    {
        canvasObj.enabled = false; // the message that we can press the lever is disabled at the start
    }

    void Update()
    {
        isEPressed = Input.GetKey(KeyCode.E); // check of the key is pressed
    }

    private void OnTriggerEnter(Collider other)
    {
        ShowCanvas(); // show the text that the user is elligible to move the lever
    }

    private void OnTriggerStay(Collider other)
    {
        HandleLeverRotation();
    }

    private void OnTriggerExit(Collider other)
    {
        HideCanvas(); //hides canvas when we leave the trigger box near the lever
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

    private void HandleLeverRotation()
    {
        if (isEPressed && Time.time - lastEPressTime >= delayBetweenPresses)
        {
            if (leverSoundEffect != null)
            {
                leverSoundEffect.Play();
            }

            lastEPressTime = Time.time;

            RotateLever();

            isEPressed = false;

            UpdateCounters();
            leverChecker.OnLeverChanged();
        }
    }

    private void RotateLever()
    {
        float rotationAngle = middle ? 45f : -45f;
        lever.transform.Rotate(new Vector3(rotationAngle, 0f, 0f), Space.Self);
        middle = !middle;
    }

    private void UpdateCounters() //updates the counters when called
    {
        if (gameObject.name == "OrangeSwitch") counterO++;
        else if (gameObject.name == "GreenSwitch") counterG++;
        else if (gameObject.name == "RedSwitch") counterR++;
        else if (gameObject.name == "WhiteSwitch") ResetCounters(); //reset the counters when the white is pressed
    }

    private void ResetCounters()
    {
        counterR = 0;
        counterG = 0;
        counterO = 0;
    }

    public static int CounterR { get { return counterR; } }
    public static int CounterG { get { return counterG; } }
    public static int CounterO { get { return counterO; } }
}
