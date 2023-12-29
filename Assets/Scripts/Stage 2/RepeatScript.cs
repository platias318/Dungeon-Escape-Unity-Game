using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatScript : MonoBehaviour
{
  

    public PianoScript piano;
 


    private void Start()
    {
       

    }
    private void OnCollisionEnter(Collision other)
    {

       
        Debug.Log("Repeat");

        piano.repeat();

    }
}
