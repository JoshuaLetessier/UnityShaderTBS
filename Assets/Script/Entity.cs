using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField]  public int _health;
    [SerializeField]  public int _maxHealth;
    [SerializeField] public int _mana;
    [SerializeField] public int _maxMana;
    [SerializeField]  List<Competence> _competences;
    


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

    public void UseMana(int mana)
    {
        _mana -= mana;
        if (_mana <= 0)
        {
            _mana = 0;
        }
    }

    public void GainMana(int mana)
    {
        _mana += mana;
        if (_mana > _maxMana)
        {
            _mana = _maxMana;
        }
    }


    public bool IsDefeated()
    {
        return _health <= 0;
    }

    public void UseCompetence(Competence competence, Entity entityTarget)
    {
        competence.Apply(entityTarget);
        competence.StartCooldown();
    }

    public void UpdateCooldowns()
    {
        if (_competences != null) return;
        foreach (var competence in _competences)
        {
            competence.ReduceCooldown();
        }
    }

    public List<Competence> getCompetence()
    {
        return _competences;
    }



}
