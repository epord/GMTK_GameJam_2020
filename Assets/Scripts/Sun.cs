using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision with sun");
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ErrorActivator>().Lose();
        }
    }
}
