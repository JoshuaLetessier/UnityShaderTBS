using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionIndicator : MonoBehaviour
{
    [SerializeField] private MeshRenderer _selectionCircle;
    [SerializeField] private MeshRenderer _selectionArrow;
    [SerializeField] [Range(0, 1)] public float _defaultOpacity;
    [Range(0, 1)] private float _opacity;

    public void Awake()
    {
        SetOpacity(_defaultOpacity);
    }

    public void SetOpacity(float opacity)
    {
        _selectionCircle.material.SetFloat("_Opacity", opacity);
        _selectionArrow.material.SetFloat("_Opacity", opacity);
        _opacity = opacity;
    }

    void FixedUpdate()
    {
        Camera cam = Camera.main;
        //transform.rotation.SetLookRotation(cam.transform.position - transform.position);
        transform.LookAt(new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
    }
}
