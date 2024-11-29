using Cinemachine;
using System;
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

    [SerializeField] UnityEvent SlashUEvent;
    [SerializeField] UnityEvent LightenedSlashUEvent;
    [SerializeField] UnityEvent EstocUEvent;
    [SerializeField] Animator _animator;
    [SerializeField] Material _baseMaterial;
    [SerializeField] Renderer _blade;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    [SerializeField] ParticleSystem _thunderBuffEmitter;

    bool _isLightened = false;

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
        _virtualCamera.Priority = 20;
        transform.LookAt(_target.transform.position);
        GoTo(_target.transform.position);
        if(false == _isLightened)
        {
            _animator.SetTrigger("Slash");
        }
        else
        {
            _animator.SetTrigger("LightenedSlash");
        }
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
        if(_isLightened)
        {
            _isLightened = false;
            _blade.material = _baseMaterial;
            _thunderBuffEmitter.Stop();
        }
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
        _virtualCamera.Priority = 0;

        Exit();
    }

    public void TriggerSlash()
    {
        if (_isLightened)
            LightenedSlashUEvent.Invoke();
        else
            SlashUEvent.Invoke();
    }

    public void Enlight()
    {
        _isLightened = true;
    }

    public void TriggerEstoc()
    {
        EstocUEvent.Invoke();
    }
}
