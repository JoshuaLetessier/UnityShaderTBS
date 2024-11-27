using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slash : Competence
{
    private Entity _target;

    public override string Name { get => "Slash"; }
    public override int Cost { get => 0; }
    public override int Damage { get => 10; }
    public override int Cooldown { get => 0; }

    public Slash(Entity entity) : base(entity) { }

    [SerializeField] Animator _animator;
    [SerializeField] UnityEvent uEvent;

    Vector3 _basePos;
    Quaternion _baseRotation;

    public override void Prepare()
    {
        Team oppositeTeam = _entity.Team.CombatManager.GetOppositeTeam(_entity.Team);
        _entity.Team.RequestEntityTarget(new RequestEntityTargetDescriptor(oppositeTeam.Entities, OnTargetSelected));
    }

    public override void Execute()
    {
        _basePos = transform.position;
        _baseRotation = transform.rotation;
        transform.LookAt(_target.transform.position);
        GoTo(_target.transform.position);
        _animator.SetTrigger("Slash");
    }

    public override void Exit()
    {
        _target.Deselect();
        _entity.OnActionDone();
    }

    public void OnTargetSelected(Entity target)
    {
        _target = target;
        Execute();
    }

    public void GoTo(Vector3 target)
    {

        StartCoroutine(GoToCoroutine(target));
    }

    IEnumerator GoToCoroutine(Vector3 target)
    {

        float dist = 100;

        while (dist > 1)
        {
            Vector3 distVect = target - transform.position;
            distVect.y = 0;
            dist = distVect.magnitude;
            transform.position += Vector3.Normalize(distVect) * Time.deltaTime * 5;
            yield return null;
        }
        _animator.SetTrigger("AtDestination");
    }

    public void BackToOriginalPos()
    {
        StartCoroutine(BackToPosCoroutine());
    }

    IEnumerator BackToPosCoroutine()
    {

        float dist = 100;

        while (dist > 0.1f)
        {
            Vector3 distVect = _basePos - transform.position;
            distVect.y = 0;
            dist = distVect.magnitude;
            transform.position += Vector3.Normalize(distVect) * Time.deltaTime * 5;
            yield return null;
        }
        transform.position = _basePos;
        transform.rotation = _baseRotation;
        _animator.SetTrigger("AtDestination");

        Exit();
    }

    public void TriggerSlash()
    {
        uEvent.Invoke();
    }
}
