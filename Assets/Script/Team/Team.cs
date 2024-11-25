using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Team : MonoBehaviour
{

    public List<GameObject> _entities;
    public GameObject _spawnPointsContainer;

    public void Spawn()
    {
        Transform[] spawnPoints = _spawnPointsContainer.GetComponentsInChildren<Transform>();
        // The 1 increments on spawnPoint index are meant to avoid using the container itself as a spawn point.
        for ( int i = 0; i < Mathf.Min(_entities.Count, spawnPoints.Length - 1); i++)
        {
            Instantiate(_entities[i], spawnPoints[i + 1].position, spawnPoints[i + 1].rotation, transform);
        }
    }

    virtual public void OnStartTurn() { }
    virtual public void OnEndTurn() { }
    virtual public void HandleUpdate() { }

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

    public bool IsGameObjectInTeam(GameObject other)
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
}
