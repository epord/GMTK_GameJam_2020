using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorActivator : MonoBehaviour
{
    public Error[] errors;

    public void FixAll()
    {
        foreach (Error error in errors)
        {
            error.Deactivate();
        }
    }

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
        if (Input.GetKeyDown("6"))
        {
            errors[5].Toggle();
        }
        if (Input.GetKeyDown("7"))
        {
            errors[6].Toggle();
        }
        if (Input.GetKeyDown("8"))
        {
            errors[7].Toggle();
        }
        if (Input.GetKeyDown("9"))
        {
            errors[8].Toggle();
        }
        if (Input.GetKeyDown("0"))
        {
            errors[9].Toggle();
        }
    }
}
