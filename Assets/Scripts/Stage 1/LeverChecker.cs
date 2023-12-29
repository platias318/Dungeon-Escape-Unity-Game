using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverChecker : MonoBehaviour
{
    [SerializeField] private int OrangeCode; //the int code for the orange coloured lever
    [SerializeField] private int GreenCode; //the int code for the green coloured lever
    [SerializeField] private int RedCode; //the int code for the red coloured lever
    [SerializeField] private GameObject door; //the door that the levers are gonna be opening when pressed in the right amount of times
    [SerializeField] private AudioSource doorUnlocked; //the sound when the door is unlocked with success
    private bool hasPlayed = false; //boolean that is used to see if the audio effect of the door being unlocked has been played

    // Unity Event to be triggered when levers are changed
    public UnityEvent onLeverChanged;

    private void Start()
    {
        if (door.GetComponent<Door>().enabled == true)
        {
            door.GetComponent<Door>().enabled = false;
        }

        // Subscribe to the onLeverChanged event
        onLeverChanged.AddListener(CheckLeverCodes);
    }

    // Call this method when levers are changed
    public void OnLeverChanged()
    {
        onLeverChanged.Invoke();
    }

    // Check lever codes and update the door state accordingly
    private void CheckLeverCodes()
    {
        if (Lever2.CounterG == GreenCode && Lever2.CounterO == OrangeCode && Lever2.CounterR == RedCode)
        {
            Debug.Log("levers are good");
            if (!doorUnlocked.isPlaying && !hasPlayed)
            {
                hasPlayed = true;
                doorUnlocked.Play();
            }
            if (door.GetComponent<Door>().enabled == false)
            {
                Debug.Log("enables the door opener");
                door.GetComponent<Door>().enabled = true;
            }
        }
        else
        {
            if (door.GetComponent<Door>().enabled == true)
            {
                door.GetComponent<Door>().enabled = false;
                hasPlayed = false;
            }
        }
    }
}