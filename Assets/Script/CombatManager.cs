using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    [Header("Combat Logic")]

    [SerializeField] Team _playerTeam;
    [SerializeField] Team _enemyTeam;
    private Team _currentTeam;

    [Header("Combat Zone Parameters")]

    [SerializeField] List<GameObject> _inactiveOnCombatStart;
    [SerializeField] List<GameObject> _activeOnCombatEnd;


    private Action<GameObject> _onClickSelectAction;
    private Action<GameObject> _onHoverSelectAction;

    private void Start()
    {
        StartCombat();
    }

    public void StartCombat()
    {
        foreach (GameObject gameObject in _inactiveOnCombatStart)
        {
            gameObject.SetActive(false);
        }

        GetComponent<BoxCollider>().enabled = false;

        InputManager inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        inputManager.AddOnMouseUpAction(OnClick);
        inputManager.AddOnMouseMoveAction(OnMouveMove);

        _playerTeam.Spawn();
        _enemyTeam.Spawn();
        _currentTeam = _playerTeam;
        _currentTeam.OnStartTurn();
    }

    public void AbortCombat(bool resetCombatZone = true)
    {
        if(resetCombatZone)
        {
            foreach (GameObject gameObject in _inactiveOnCombatStart)
            {
                gameObject.SetActive(true);
            }
            GetComponent<BoxCollider>().enabled = true;
        } 
        else EndCombat();
    }

    public void EndCombat()
    {
        foreach (GameObject gameObject in _activeOnCombatEnd)
        {
            gameObject.SetActive(true);
        }
    }

    public void EndTurn()
    {
        _currentTeam.OnEndTurn();
        _currentTeam = GetOppositeTeam(_currentTeam);
        _currentTeam.OnStartTurn();
    }

    public Team GetOppositeTeam(Team team)
    {
        if (team == _playerTeam)
        {
            return _enemyTeam;
        }
        else
        {
            return _playerTeam;
        }
    }

    #region CombatInputs 

    public void OnClick()
    {
        if (_onClickSelectAction == null) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            GameObject s = hit.collider.gameObject;
            _onClickSelectAction.Invoke(s);
        } else
        {
            _onClickSelectAction.Invoke(null);
        }
    }

    public void OnMouveMove()
    {
        if (_onHoverSelectAction == null) return;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject s = hit.collider.gameObject;
            _onHoverSelectAction.Invoke(s);
        } else
        {
            _onHoverSelectAction.Invoke(null);
        }
    }

    public void SubscribeOnClickSelect(Action<GameObject> action)
    {
        _onClickSelectAction += action;
    }

    public void UnsubscribeOnClickSelect(Action<GameObject> action)
    {
        _onClickSelectAction -= action;
    }

    public void SubscribeOnHoverSelect(Action<GameObject> action)
    {
        _onHoverSelectAction += action;
    }

    public void UnsubscribeOnHoverSelect(Action<GameObject> action)
    {
        _onHoverSelectAction -= action;
    }

    #endregion CombatInputs 
}