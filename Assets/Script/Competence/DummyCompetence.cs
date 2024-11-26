using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCompetence : Competence
{
    private Entity _target;

    public override void Prepare()
    {
        _entity.Team.RequestEntityTarget(IsEnemy, OnTargetSelected);
    }

    public bool IsEnemy(Entity other)
    {
        return !_entity.IsSameTeam(other);
    }

    public override void Execute()
    {
        Exit();
    }

    public override void Exit()
    {
        Debug.Log("Dummy Competence Executed");
        _entity.OnActionDone();
    }

    public void OnTargetSelected(Entity target)
    {
        _target = target;
        Execute();
    }
}
