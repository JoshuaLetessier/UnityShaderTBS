using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battlecry : Competence
{
    private Entity _target;

    public override string Name { get => "Battlecry"; }
    public override int Cost { get => 0; }
    public override int Damage { get => 10; }
    public override int Cooldown { get => 0; }

    public Battlecry(Entity entity) : base(entity) { }

    [SerializeField] Animator _animator;
    [SerializeField] UnityEvent uEvent;

    Vector3 _basePos;
    Quaternion _baseRotation;

    public override void Prepare()
    {
        Execute();
    }

    public override void Execute()
    {
        _animator.SetTrigger("Battlecry");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Exit();
    }

    public override void Exit()
    {
        _entity.OnActionDone();
    }

    public void TriggerThunder()
    {
        uEvent.Invoke();
    }
}
