using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerTeamState
{
    SELECTING_CHARACTER,
    SELECTING_ACTION,
    PREPARING_ACTION,
    EXECUTING_ACTION,
    WAITING
}

public struct RequestEntityTargetDescriptor
{
    public RequestEntityTargetDescriptor(List<Entity> selectableEntities, Action<Entity> onTargetFound)
    {
        this.selectableEntities = selectableEntities;
        this.onTargetFound = onTargetFound;
    }
    public List<Entity> selectableEntities;
    public Action<Entity> onTargetFound;
}

public abstract class Team : MonoBehaviour
{

    protected PlayerTeamState _state = PlayerTeamState.WAITING;

    public PlayerTeamState State { get => _state; set => _state = value; }

    [SerializeField] private List<GameObject> _entitiesPrefabs;

    protected List<Entity> _entities = new List<Entity>();
    public List<Entity> Entities { get { return _entities; } }

    public GameObject _spawnPointsContainer;

    [SerializeField] protected CombatManager _combatManager;
    public CombatManager CombatManager { get => _combatManager; }

    public void Spawn()
    {
        Transform[] spawnPoints = _spawnPointsContainer.GetComponentsInChildren<Transform>();
        // The 1 increments on spawnPoint index are meant to avoid using the container itself as a spawn point.
        for ( int i = 0; i < Mathf.Min(_entitiesPrefabs.Count, spawnPoints.Length - 1); i++)
        {
            Entity spawnedEntity = Instantiate(_entitiesPrefabs[i], spawnPoints[i + 1].position, spawnPoints[i + 1].rotation, transform).GetComponent<Entity>();
            spawnedEntity.Team = this;
            _entities.Add(spawnedEntity);
        }
    }

    virtual public void OnStartTeamTurn() { }
    virtual public void OnEndTurn() { }
    virtual public void OnEntityTurnDone() { }

    public bool AllEntitiesDefeated()
    {
        foreach (Entity _entitie in _entities)
        {
            if (!_entitie.GetComponent<Entity>().IsDefeated())
            {
                return false;
            }
        }
        return true;
    }

    public abstract void Defeated();

    public abstract void RequestEntityTarget(RequestEntityTargetDescriptor requestDescriptor);

}
