using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Camera targetCamera;

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main; 
        }
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + targetCamera.transform.forward);
    }
}
