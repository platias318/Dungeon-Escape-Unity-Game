using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNote : MonoBehaviour
{
    public AudioSource aud;
    public float delay;

    public PianoScript piano;
    public string note;


    private void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
        
    }
    private void OnCollisionEnter(Collision other)
    {
        
        aud.Play();
        Debug.Log("Audio Played");

        piano.PressPlate(note);

    }


}
