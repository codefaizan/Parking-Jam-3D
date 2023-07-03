using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField] Animator animator;
    int carCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        carCount++;
        if(carCount == 1)
            animator.SetBool("BarUp", true);
    }

    private void OnTriggerExit(Collider other)
    {
        carCount--;
        if(carCount==0)
         animator.SetBool("BarUp", false);
    }
}
