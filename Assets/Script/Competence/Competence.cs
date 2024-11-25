using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Competence : MonoBehaviour
{

    public string Name { get; protected set; } 
    public int Cost { get; protected set; }
    public int Cooldown { get; protected set; } 
    public int CurrentCooldown { get; private set; } 

    public Competence(string name, int cost, int cooldown)
    {
        Name = name;
        Cost = cost;
        Cooldown = cooldown;
        CurrentCooldown = 0;
    }

    public abstract void Apply(Entity caster, Entity target);

    public virtual bool IsUsable(Entity caster)
    {
        return caster._health > 0 && CurrentCooldown <= 0;
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
