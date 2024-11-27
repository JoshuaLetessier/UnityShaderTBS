using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHPBar : MonoBehaviour
{

    [SerializeField] Material _material;
    private Entity _entity;


    private void Awake()
    {
        _material = new Material(_material);
    }

    // Start is called before the first frame update
    void Start()
    {
        _entity.GetComponentInParent<Entity>();
        _material.SetFloat("_Health", _entity._maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        _material.SetFloat("_Health", _entity._health);
    }
}
