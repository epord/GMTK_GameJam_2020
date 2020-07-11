using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorActivator : MonoBehaviour
{
    public Error[] errors;


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            errors[0].Toggle();
        }
        if (Input.GetKeyDown("2"))
        {
            errors[1].Toggle();
        }
        if (Input.GetKeyDown("3"))
        {
            errors[2].Toggle();
        }
        if (Input.GetKeyDown("4"))
        {
            errors[3].Toggle();
        }
        if (Input.GetKeyDown("5"))
        {
            errors[4].Toggle();
        }
    }
}
