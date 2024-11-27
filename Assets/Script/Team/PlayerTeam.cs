
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

class PlayerTeam : Team
{
    private PlayerTeamState _state = PlayerTeamState.WAITING;
    private GameObject _selectedCharacter = null;
    private GameObject _hoveredCharacter = null;
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
        if (_selectedCharacter.TryGetComponent(out Character script))
            script.ResetSelection();
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
        if (clickedObject != null && IsTeamMember(clickedObject) && _hoveredCharacter?.GetInstanceID() != clickedObject.GetInstanceID())
        {
            OnCharacterHovered(clickedObject);
        } else if(_hoveredCharacter != null && _hoveredCharacter.GetInstanceID() != clickedObject?.GetInstanceID())
        {
            OnHoverExit();
        }
    }

    void OnCharacterSelected(GameObject selectedCharacter)
    {
        _selectedCharacter = selectedCharacter;
        _state = PlayerTeamState.SELECTING_ACTION;
        _combatManager.UnsubscribeOnClickSelect(SC_OnClick);
        _combatManager.UnsubscribeOnHoverSelect(SC_OnHover);
        if (_selectedCharacter.TryGetComponent(out Character script))
            script.ConfirmSelection();
        _selectedAction = new DummyAction(this, _combatManager);
        _selectedAction.Prepare();
    }

    void OnCharacterHovered(GameObject hoveredCharacter)
    {
        _hoveredCharacter = hoveredCharacter;
        if (_hoveredCharacter.TryGetComponent(out Character script))
            script.Select();
    }

    void OnHoverExit()
    {
        if (_hoveredCharacter.TryGetComponent(out Character script))
            script.ResetSelection();
        _hoveredCharacter = null;
    }

    #endregion SelectingCharacterState


    public override void Defeated()
    {
        // Game Over
    }
}