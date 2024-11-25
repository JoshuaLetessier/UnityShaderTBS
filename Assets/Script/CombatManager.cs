using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    [SerializeField] Team _playerTeam;
    [SerializeField] Team _enemyTeam;
    private Team _currentTeam;

    private Action<GameObject> _onClickSelectAction;
    private Action<GameObject> _onHoverSelectAction;

    private void Start()
    {
        StartCombat();
    }

    public void StartCombat()
    {
        _playerTeam.Spawn();
        _enemyTeam.Spawn();
        _currentTeam = _playerTeam;
        _currentTeam.OnStartTurn();

        InputManager inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        inputManager.AddOnMouseUpAction(OnClick);
        inputManager.AddOnMouseMoveAction(OnMouveMove);
    }

    void Update()
    {
        _currentTeam.HandleUpdate();
    }

    void EndTurn()
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
        Debug.Log("Click");
        if (_onClickSelectAction == null) return;
        Debug.Log("Selection Actions Not Empty");
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit");
            GameObject s = hit.collider.gameObject;
            _onClickSelectAction.Invoke(s);
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
        }
    }

    public void AddOnClickSelectAction(Action<GameObject> action)
    {
        Debug.Log("Loaded new acton");
        _onClickSelectAction += action;
    }

    public void RemoveOnClickSelectAction(Action<GameObject> action)
    {
        _onClickSelectAction -= action;
    }

    public void AddOnHoverSelectAction(Action<GameObject> action)
    {
        _onHoverSelectAction += action;
    }

    public void RemoveOnHoverSelectAction(Action<GameObject> action)
    {
        _onHoverSelectAction -= action;
    }

    #endregion CombatInputs 
}
