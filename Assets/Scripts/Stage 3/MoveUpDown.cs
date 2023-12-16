using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float amplitude = 3.0f;
    [SerializeField] float minY = 0.0f;
    [SerializeField] float maxY = 4.0f;

    void Update()
    {
        float newY = Mathf.Clamp(amplitude * Mathf.Sin(Time.time * speed), minY, maxY);
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}


