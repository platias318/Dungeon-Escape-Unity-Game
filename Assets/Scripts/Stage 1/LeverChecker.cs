using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverChecker : MonoBehaviour
{
    [SerializeField] private int OrangeCode;
    [SerializeField] private int GreenCode;
    [SerializeField] private int RedCode;
    // Update is called once per frame
    void Update()
    {
        if (Lever2.CounterG==GreenCode && Lever2.CounterO==OrangeCode && Lever2.CounterR == RedCode)
        {
           //something will happen
        }
    }
}
