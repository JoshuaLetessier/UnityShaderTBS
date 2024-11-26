using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            OnTargetSelected(clickedObject);
        }
    }
    void OnHover(GameObject clickedObject)
    {
        if (clickedObject != null && _combatManager.GetOppositeTeam(_playerTeam).IsTeamMember(clickedObject))
        {
            if (_selectedTarget?.GetInstanceID() != clickedObject.GetInstanceID()) OnTargetHovered(clickedObject);
        }
        else if (_selectedTarget != null && _selectedTarget.GetInstanceID() != clickedObject?.GetInstanceID())
        {
            OnHoverExit();
        }
    }
    void OnTargetSelected(GameObject selectedCharacter)
    {
        _selectedTarget = selectedCharacter;
        _combatManager.UnsubscribeOnClickSelect(OnClick);
        _combatManager.UnsubscribeOnHoverSelect(OnHover);
        _selectedTarget.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        Execute();
    }

    void OnTargetHovered(GameObject selectedCharacter)
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