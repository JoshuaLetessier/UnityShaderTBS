using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team : MonoBehaviour
{

    public List<Entity> _entities;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(AllEntitiesDefeated())
        {
            Defeated();
        }

    }
    public bool AllEntitiesDefeated()
    {
        foreach (Entity _entitie in _entities)
        {
            if (!_entitie.IsDefeated())
            {
                return false;
            }
        }
        return true;
    }

    public Entity SelectEntity()
    {
        return _entities[Random.Range(0, _entities.Count)];
    }

    public abstract void Defeated();
}
