using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCompetence : Competence
{
    private Entity _target;

    public override string Name { get => "Dummy Competence"; }
    public override int Cost { get => 0; }
    public override int Damage { get => 10; }
    public override int Cooldown { get => 0; }

    public DummyCompetence(Entity entity) : base(entity) { }

    public override void Prepare()
    {
        Team oppositeTeam = _entity.Team.CombatManager.GetOppositeTeam(_entity.Team);
        _entity.Team.RequestEntityTarget(new RequestEntityTargetDescriptor(oppositeTeam.Entities, OnTargetSelected));
    }

    public override void Execute()
    {
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
