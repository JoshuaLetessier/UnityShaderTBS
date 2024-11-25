using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    public void Close()
    {
        menu.SetActive(false);
    }
}
