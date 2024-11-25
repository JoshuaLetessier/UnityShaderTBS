using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCompetenceMenu : MonoBehaviour
{
    [SerializeField] GameObject _competenceMenu;

    public void ShowMenu()
    {
        _competenceMenu.SetActive(true);
    }
}
