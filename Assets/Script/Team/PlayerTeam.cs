
using System;
using UnityEngine;

public struct RequestEntityTargetDescriptor
{
    public Func<Entity, bool> predicate;
    public Action<Entity> onTargetFound;
}


class PlayerTeam : Team
{
    
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
        _selectedCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        //_selectedAction = new DummyAction(this, _combatManager);
        //_selectedAction.Prepare();
        _combatManager.ShowCompetenceMenu.ShowMenu(_selectedCharacter.GetComponent<Entity>());
    }

    void OnCharacterHovered(GameObject hoveredCharacter)
    {
        _hoveredCharacter = hoveredCharacter;
        _hoveredCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
    }

    void OnHoverExit()
    {
        _hoveredCharacter.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        _hoveredCharacter = null;
    }

    #endregion SelectingCharacterState


    public override void RequestEntityTarget(RequestEntityTargetDescriptor requestEntityTargetDescriptor)
    {
        if(_requestEntityTargetDescriptor != null)
        RequestEntityTargetDescriptor _requestEntityTargetDescriptor = requestEntityTargetDescriptor;

        Action<GameObject> action = (GameObject selectedGameObject) =>
        {
            if(selectedGameObject != null && selectedGameObject.TryGetComponent(out Entity selectedCharacter))
            {
                if (predicate(selectedGameObject))
                {
                    onTargetFound(selectedCharacter);
                }
            }
                if (predicate(selectedGameObject))
            {
                onTargetFound(selectedCharacter.GetComponent<Entity>());
            }
        };
    }

    public void OnSelectedEntityCandidate(Entity selectionCandidate)
    {
        if(selectionCandidate != null && _requestPredicate(selectionCandidate))
        {

        }
    }

    public override void Defeated()
    {
        // Game Over
    }
}