using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Competence : EntityAction
{
    protected Entity _entity;

    public Entity Entity { get => _entity; set => _entity = value; }

    public abstract string Name { get; } 
    public abstract int Cost { get; }
    public abstract int Damage { get; }
    public abstract int Cooldown { get; } 
    public int CurrentCooldown { get; private set; }

    public Competence(Entity entity)
    {
        _entity = entity;
    }

    public virtual bool IsUsable()
    {
        return CurrentCooldown <= 0;
    }

    public void StartCooldown()
    {
        CurrentCooldown = Cooldown;
    }

    public void ReduceCooldown()
    {
        if (CurrentCooldown > 0)
            CurrentCooldown--;
    }
}
