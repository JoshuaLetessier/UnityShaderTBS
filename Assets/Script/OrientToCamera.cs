using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientToCamera : MonoBehaviour
{
    private void FixedUpdate()
    {
        Camera cam = Camera.main;
        transform.rotation.SetLookRotation(cam.transform.position - transform.position);
    }
}
