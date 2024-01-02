using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] float amplitude = 6.0f;
    [SerializeField] float minY = -2.0f;
    [SerializeField] float maxY = 4.0f;
    [SerializeField] private GameObject switcher;

    void Update()
    {
        if (switcher.gameObject.GetComponent<Switch>().IsEnabled())
        {
            float newY = Mathf.Clamp(amplitude * Mathf.Sin(Time.time * speed), minY, maxY);
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
    }
}


