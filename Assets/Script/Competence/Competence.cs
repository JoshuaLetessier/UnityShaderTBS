using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Competence : ScriptableObject
{
    public string Name { get; protected set; } 
    public int Cost { get; protected set; }
    public int Damage { get; protected set; }
    public int Cooldown { get; protected set; } 
    public int CurrentCooldown { get; private set; }

    public void Init(string name, int cost, int damage, int cooldown)
    {
        Name = name;
        Cost = cost;
        Damage = damage;
        Cooldown = cooldown;
        CurrentCooldown = 0;
    }

    public abstract void Apply(Entity target);

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
