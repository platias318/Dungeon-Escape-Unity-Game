using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverChecker : MonoBehaviour
{
    [SerializeField] private int OrangeCode;
    [SerializeField] private int GreenCode;
    [SerializeField] private int RedCode;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource doorUnlocked;
    private bool hasPlayed = false;

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