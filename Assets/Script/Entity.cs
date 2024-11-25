using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public int _health;
    public int _maxHealth;
    List<Competence> _competences;


    public Entity(int health, int maxHealth, List<Competence> competences)
    {
        this._health = health;
        this._maxHealth = maxHealth;
        this._competences = competences;
    }

    public void Start()
    {
        _health = _maxHealth;
    }

    public void Update() {
        //UpdateCooldowns();
    }


    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        _health += heal;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public bool IsDefeated()
    {
        return _health <= 0;
    }

    public void UseCompetence(Competence competence, Entity entityTarget)
    {
        if (competence.IsUsable(this))
        {
            competence.Apply(this, entityTarget);
            competence.StartCooldown();
        }
    }

    public void UpdateCooldowns()
    {
        if (_competences != null) return;
        foreach (var competence in _competences)
        {
            competence.ReduceCooldown();
        }
    }



}
