
using System;
using System.Collections.Generic;
using UnityEngine;




class PlayerTeam : Team
{
    
    private Entity _currentEntity = null;
    private int _currentEntityIndex;
    private Entity _hoveredEntity = null;
    
    RequestEntityTargetDescriptor _requestEntityTargetDescriptor;

    public override void OnStartTeamTurn()
    {
        _state = PlayerTeamState.SELECTING_CHARACTER;
        _currentEntityIndex = 0;
        _currentEntity = _entities[_currentEntityIndex];
        _currentEntity.Select();
        _combatManager.ShowCompetenceMenu.ShowMenu(_currentEntity);
    }

    void StartNextEntityTurn()
    {
        _currentEntityIndex++;
        _currentEntity = _entities[_currentEntityIndex];
        _currentEntity.Select();
        _combatManager.ShowCompetenceMenu.ShowMenu(_currentEntity);
    }

    public override void OnEntityTurnDone()
    {
        _currentEntity.Deselect();
        if(_currentEntityIndex < _entities.Count - 1)
        {
            _state = PlayerTeamState.SELECTING_CHARACTER;
            StartNextEntityTurn();
        }
        else
        {
            _state = PlayerTeamState.WAITING;
            _combatManager.EndTeamTurn();
        }
    }

    public override void RequestEntityTarget(RequestEntityTargetDescriptor requestDescriptor)
    {
        _requestEntityTargetDescriptor = requestDescriptor;
        _combatManager.SubscribeOnClickSelect(OnSelectedEntityCandidate);
        _combatManager.SubscribeOnHoverSelect(OnSelectedEntityCandidateHovered);
    }

    void OnSelectedEntityCandidateHovered(Entity hoveredEntity)
    {
        if (_hoveredEntity != hoveredEntity && IsEntityInList(_requestEntityTargetDescriptor.selectableEntities, hoveredEntity))
        {
            _hoveredEntity = hoveredEntity;
            _hoveredEntity.HoverSelect();
        } else if (_hoveredEntity != hoveredEntity)
        {
            _hoveredEntity?.Deselect();
            _hoveredEntity = null;
        }
    }

    public void OnSelectedEntityCandidate(Entity selectionCandidate)
    {
        if (selectionCandidate != null && IsEntityInList(_requestEntityTargetDescriptor.selectableEntities, selectionCandidate))
        {
            selectionCandidate.Select();
            _combatManager.UnsubscribeOnClickSelect(OnSelectedEntityCandidate);
            _combatManager.UnsubscribeOnHoverSelect(OnSelectedEntityCandidateHovered);
            _requestEntityTargetDescriptor.onTargetFound(selectionCandidate);
        }
    }

    public bool IsEntityInList(List<Entity> entities, Entity other)
    {
        if (entities == null) return false;
        return entities.Contains(other);
    }


    public override void Defeated()
    {
        // Game Over
    }
}