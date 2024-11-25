
using System;
using UnityEngine;

public enum PlayerTeamState
{
    SELECTING_CHARACTER,
    SELECTING_ACTION,
    PREPARING_ACTION,
    EXECUTING_ACTION,
    WAITING
}

abstract class CharacterAction
{
    abstract public void Prepare();

    abstract public void Execute();
}

class PlayerTeam : Team
{
    private PlayerTeamState _state = PlayerTeamState.WAITING;
    private GameObject _selectedCharacter = null;
    private CharacterAction _selectedAction = null;

    public CombatManager _combatManager;

    private Action<GameObject> SC_OnClickRef;
    private Action<GameObject> SC_OnHoverRef;

    private void Awake()
    {
        SC_OnClickRef = (GameObject clickedObject) => SC_OnClick(clickedObject);
        SC_OnHoverRef = (GameObject clickedObject) => SC_OnHover(clickedObject);
    }

    private bool CharacterSelectedCondition(GameObject clickedObject)
    {
        return clickedObject != null && IsGameObjectInTeam(clickedObject);
    }

    public override void OnStartTurn()
    {
        SC_OnClickRef = (GameObject clickedObject) => SC_OnClick(clickedObject);
        SC_OnHoverRef = (GameObject clickedObject) => SC_OnHover(clickedObject);
        _state = PlayerTeamState.SELECTING_CHARACTER;
        _combatManager.AddOnClickSelectAction(SC_OnClickRef);
        _combatManager.AddOnHoverSelectAction(SC_OnHoverRef);
    }

    #region SelectingCharacterState

    void SC_OnClick(GameObject clickedObject)
    {
        if (clickedObject != null && IsGameObjectInTeam(clickedObject))
        {
            OnCharacterSelected(clickedObject);
        }
    }

    void SC_OnHover(GameObject clickedObject)
    {
        if (clickedObject != null && IsGameObjectInTeam(clickedObject))
        {
            OnCharacterHovered(clickedObject);
        } else if(_selectedCharacter != null)
        {
            _selectedCharacter.GetComponent<MeshRenderer>().material.color = Color.white;
            _selectedCharacter = null;
            // remove hover effect
        }
    }

    void OnCharacterSelected(GameObject selectedCharacter)
    {
        _selectedCharacter = selectedCharacter;
        _state = PlayerTeamState.SELECTING_ACTION;
        _combatManager.RemoveOnClickSelectAction(SC_OnClickRef);
        _combatManager.RemoveOnHoverSelectAction(SC_OnHoverRef);
    }

    void OnCharacterHovered(GameObject selectedCharacter)
    {
        _selectedCharacter = selectedCharacter;
        _selectedCharacter.GetComponent<MeshRenderer>().material.color = Color.green;
        _state = PlayerTeamState.SELECTING_ACTION;
    }

    #endregion SelectingCharacterState


    public override void Defeated()
    {
        // Game Over
    }
}