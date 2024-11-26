using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject _selector;

    private void Start()
    {
        _selector.SetActive(false);
    }

    public void Select() 
    {
        _selector.SetActive(true);
    }

    public void ConfirmSelection()
    {
        _selector.GetComponent<Animator>().speed = 0;
    }

    public void ResetSelection()
    {
        _selector.GetComponent<Animator>().speed = 1;
        _selector.SetActive(false);
    }
}
