
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

    abstract public void Exit();
}
class DummyAction : CharacterAction
{
    private PlayerTeam _playerTeam;
    private CombatManager _combatManager;
    private GameObject _selectedTarget = null;

    public DummyAction(PlayerTeam playerTeam, CombatManager combatManager)
    {
        _playerTeam = playerTeam;
        _combatManager = combatManager;
    }

    override public void Prepare()
    {
        _combatManager.SubscribeOnClickSelect(OnClick);
        _combatManager.SubscribeOnHoverSelect(OnHover);
    }

    override public void Execute()
    {
        Exit();
    }

    override public void Exit()
    {
        Debug.Log("Dummy Action Executed");
        _selectedTarget.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        _playerTeam.OnActionDone();
    }

    void OnClick(GameObject clickedObject)
    {
        if (clickedObject != null && _combatManager.GetOppositeTeam(_playerTeam).IsTeamMember(clickedObject))
        {
            OnCharacterSelected(clickedObject);
        }
    }
    void OnHover(GameObject clickedObject)
    {
        if (clickedObject != null && _combatManager.GetOppositeTeam(_playerTeam).IsTeamMember(clickedObject))
        {
            OnCharacterHovered(clickedObject);
        }
        else if (_selectedTarget != null)
        {
            OnHoverExit();
        }
    }

    void OnCharacterSelected(GameObject selectedCharacter)
    {
        _selectedTarget = selectedCharacter;
        _combatManager.UnsubscribeOnClickSelect(OnClick);
        _combatManager.UnsubscribeOnHoverSelect(OnHover);
        _selectedTarget.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        Execute();
    }

    void OnCharacterHovered(GameObject selectedCharacter)
    {
        _selectedTarget = selectedCharacter;
        _selectedTarget.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }

    void OnHoverExit()
    {
        _selectedTarget.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        _selectedTarget = null;
    }
}

class PlayerTeam : Team
{
    private PlayerTeamState _state = PlayerTeamState.WAITING;
    private GameObject _selectedCharacter = null;
    private CharacterAction _selectedAction = null;

    public CombatManager _combatManager;

    public override void OnStartTurn()
    {
        _state = PlayerTeamState.SELECTING_CHARACTER;
        _combatManager.SubscribeOnClickSelect(SC_OnClick);
        _combatManager.SubscribeOnHoverSelect(SC_OnHover);
    }

    public void OnActionDone()
    {
        _state = PlayerTeamState.SELECTING_CHARACTER;
        _combatManager.SubscribeOnClickSelect(SC_OnClick);
        _combatManager.SubscribeOnHoverSelect(SC_OnHover);
        _selectedCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        _selectedCharacter = null;
    }

    #region SelectingCharacterState

    void SC_OnClick(GameObject clickedObject)
    {
        if (clickedObject != null && IsTeamMember(clickedObject))
        {
            OnCharacterSelected(clickedObject);
        }
    }

    void SC_OnHover(GameObject clickedObject)
    {
        if (clickedObject != null && IsTeamMember(clickedObject))
        {
            OnCharacterHovered(clickedObject);
        } else if(_selectedCharacter != null)
        {
            _selectedCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            _selectedCharacter = null;
        }
    }

    void OnCharacterSelected(GameObject selectedCharacter)
    {
        _selectedCharacter = selectedCharacter;
        _state = PlayerTeamState.SELECTING_ACTION;
        _combatManager.UnsubscribeOnClickSelect(SC_OnClick);
        _combatManager.UnsubscribeOnHoverSelect(SC_OnHover);
        _selectedCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        _selectedAction = new DummyAction(this, _combatManager);
        _selectedAction.Prepare();
    }

    void OnCharacterHovered(GameObject selectedCharacter)
    {
        _selectedCharacter = selectedCharacter;
        _selectedCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
    }

    #endregion SelectingCharacterState


    public override void Defeated()
    {
        // Game Over
    }
}