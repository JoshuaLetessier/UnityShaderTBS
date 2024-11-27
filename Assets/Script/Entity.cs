using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] public int _health;
    [SerializeField] public int _maxHealth;
    [SerializeField] public int _mana;
    [SerializeField] public int _maxMana;
    [SerializeField] List<Competence> _competences;

    private Team _team;
    public Team Team { get => _team; set => _team = value; }

    public List<Competence> Competences { get => _competences; }

    public void Start()
    {
        _health = _maxHealth;

        foreach (Competence competence in _competences)
        {
            competence.Entity = this;
        }
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



    public void SelectAction(Competence competence)
    {
        _team.State = PlayerTeamState.PREPARING_ACTION;
        competence.Prepare();
    }

    public void Select()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.green;
    }

    public void HoverSelect()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.cyan;
    }

    public void Deselect()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
    }

    public void OnActionDone()
    {
        _team.OnEntityTurnDone();
    }

    public void UpdateCooldowns()
    {
        if (_competences != null) return;
        foreach (var competence in _competences)
        {
            competence.ReduceCooldown();
        }
    }

    public bool IsSameTeam(Entity other)
    {
        return _team == other._team;
    }
}