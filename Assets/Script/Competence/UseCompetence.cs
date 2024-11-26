using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class UseCompetence : MonoBehaviour
{
    List<Toggle> _toggleList = new List<Toggle>();
    SelectEntity _selectEntity;

    private ShowCompetenceMenu _showCompetenceMenu;
    private int _index;

    // Update is called once per frame
    void Update()
    {
        //get child of gameObject
        //and stock it in a list
        foreach (Transform child in transform)
        {
            Toggle button = child.GetComponent<Toggle>();
            if (button != null)
            {
                _toggleList.Add(button);
            }
        }
        CheckToggle();

        if(_index != -1)
        {
            if (_toggleList[_index].isOn && _selectEntity._isSelecting)
            {
                //_selectEntity._entity.UseCompetence(_showCompetenceMenu._entity.getCompetence()[_index], _selectEntity._entity);
                _toggleList[_index].isOn = false;
            }
        }

    }

    public void CheckToggle()
    {
        if(_toggleList.Count > 0 && _selectEntity._isSelecting == false)
        {
            List<Competence> competences = _showCompetenceMenu._entity.GetCompetence();
            for (int i = 0; i < _toggleList.Count; i++)
            {
                if (DisbaleInteraction(_toggleList[i], competences[i]))
                {
                    _toggleList[i].isOn = true;
                    _index = i;
                    return;
                }
            }
        }

        _index = -1;
    }

    private bool DisbaleInteraction(Toggle button, Competence competence)
    {
        if(!competence.IsUsable())
           { button.interactable = false;
            return false;
        }
        return true;
    }

}
