using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicBow : Competence
{
    private Entity _target;

    public override string Name { get => "MagicBow"; }
    public override int Cost { get => 0; }
    public override int Damage { get => 10; }
    public override int Cooldown { get => 0; }

    public MagicBow(Entity entity) : base(entity) { }

    [SerializeField] GameObject _bow;
    [SerializeField] UnityEvent _arrowVFX;
    [SerializeField] Animator _animator;

    Quaternion _baseRotation;
    float _bowVisibility = 0;
    Coroutine _bowVisibilityHandler;

    public override void Prepare()
    {
        Team oppositeTeam = _entity.Team.CombatManager.GetOppositeTeam(_entity.Team);
        _entity.Team.RequestEntityTarget(new RequestEntityTargetDescriptor(oppositeTeam.Entities, OnTargetSelected));
    }

    public override void Execute()
    {
        _baseRotation = transform.rotation;
        transform.LookAt(_target.transform.position);
        _animator.SetTrigger("DrawArrow");
        
    }

    public override void Exit()
    {
        transform.rotation = _baseRotation;
        _target.Deselect();
        _entity.OnActionDone();
    }

    public void OnTargetSelected(Entity target)
    {
        _target = target;
        Execute();
    }

    public void SpawnBow()
    {
        if (_bowVisibilityHandler != null)
        {
            StopCoroutine(_bowVisibilityHandler);
        }
        _bowVisibilityHandler = StartCoroutine(SpawnBowCoroutin());
    }

    IEnumerator SpawnBowCoroutin()
    {
        while(_bowVisibility <= 10)
        {
            _bowVisibility += Time.deltaTime;
            print(_bowVisibility);
            _bow.GetComponent<Renderer>().material.SetFloat("_Visibility", _bowVisibility);
            yield return null;
        }
    }

    public void DespawnBow()
    {
        Exit();
        if(_bowVisibilityHandler != null)
        {
            StopCoroutine(_bowVisibilityHandler);
        }
        StartCoroutine(DespawnBowCoroutin());
    }

    IEnumerator DespawnBowCoroutin()
    {
        while (_bowVisibility >= 0)
        {
            _bowVisibility -= Time.deltaTime;
            _bow.GetComponent<Renderer>().material.SetFloat("_Visibility", _bowVisibility);
            yield return null;
        }
    }

    public void SpawnArrow()
    {
        _arrowVFX.Invoke();
    }
}
