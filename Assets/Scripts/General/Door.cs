using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] GameObject trigger;
    [SerializeField] string doorOpen;
    [SerializeField] string doorClose;
    [SerializeField] private Animator animator;


    private bool action = false;
    private bool triggered = false;
    public bool isOpen = false;

    



    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            text.gameObject.SetActive(true);
            action = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Use"))
            {
                ToggleDoor();
            }
        }
    }

    void ToggleDoor()
    {
        if (isOpen)
        {

        }
    }

   
}
