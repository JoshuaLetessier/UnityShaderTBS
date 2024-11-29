using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCompetence : Competence
{
    private Entity _target;

    [SerializeField] GameObject _HealEffect;

    public override string Name { get => "Heal"; }
    public override int Cost { get => 0; }
    public override int Damage { get => 10; }
    public override int Cooldown { get => 0; }

    public HealCompetence(Entity entity) : base(entity) { }

    public override void Prepare()
    {
        _entity.Team.RequestEntityTarget(new RequestEntityTargetDescriptor(_entity.Team.Entities, OnTargetSelected));
    }

    public override void Execute()
    {
        Instantiate(_HealEffect, _target.transform.position, Quaternion.identity);

        _target.Heal(Damage);
        Exit();
    }

    public override void Exit()
    {
        _target.Deselect();
        Debug.Log("Dummy Competence Executed");
        _entity.OnActionDone();
    }

    public void OnTargetSelected(Entity target)
    {
        _target = target;
        Execute();
    }
}
