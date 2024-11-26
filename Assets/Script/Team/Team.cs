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


public abstract class Team : MonoBehaviour
{

    private PlayerTeamState _state = PlayerTeamState.WAITING;

    public PlayerTeamState State { get => _state; set => _state = value; }

    [SerializeField] private List<GameObject> _entitiesPrefabs;

    private List<GameObject> _entities = new List<GameObject>();
    public List<GameObject> Entities { get { return _entities; } }

    public GameObject _spawnPointsContainer;

    public void Spawn()
    {
        Transform[] spawnPoints = _spawnPointsContainer.GetComponentsInChildren<Transform>();
        // The 1 increments on spawnPoint index are meant to avoid using the container itself as a spawn point.
        for ( int i = 0; i < Mathf.Min(_entitiesPrefabs.Count, spawnPoints.Length - 1); i++)
        {
            _entities.Add(Instantiate(_entitiesPrefabs[i], spawnPoints[i + 1].position, spawnPoints[i + 1].rotation, transform));         
        }
    }

    virtual public void OnStartTurn() { }
    virtual public void OnEndTurn() { }

    public bool AllEntitiesDefeated()
    {
        foreach (GameObject _entitie in _entities)
        {
            if (!_entitie.GetComponent<Entity>().IsDefeated())
            {
                return false;
            }
        }
        return true;
    }

    public Entity SelectEntity()
    {
        return _entities[Random.Range(0, _entities.Count)].GetComponent<Entity>();
    }

    public abstract void Defeated();

    public bool IsTeamMember(GameObject other)
    {
        int otherInstanceId = other.GetInstanceID();
        foreach (GameObject entity in _entities)
        {
            if (entity.GetInstanceID() == otherInstanceId)
            {
                return true;
            }
        }
        return false;
    }

    public abstract void RequestEntityTarget(Func<Entity, bool> predicate, Action<Entity>onTargetFound);

}
