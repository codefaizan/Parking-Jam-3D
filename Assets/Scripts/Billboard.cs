using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Start()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
