using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireHydrant : MonoBehaviour
{
    [SerializeField] GameObject fluidStream;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            fluidStream.SetActive(true);
        }
    }
}
