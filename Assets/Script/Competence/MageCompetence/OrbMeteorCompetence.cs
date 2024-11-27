using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class OrbMeteorCompetence : Competence
{

    public override string Name { get => "OrbMeteorCompetence"; }
    public override int Cost { get => 0; }
    public override int Damage { get => 10; }
    public override int Cooldown { get => 0; }

    private GameObject _Orb;

    private List<GameObject> _meteors;
    private List<bool> _isShooting;

    MonoBehaviour _linkedMB;

    Vector3 _targetPosition;

    private Entity _target;

    public OrbMeteorCompetence(Entity entity) : base(entity)
    {
        _meteors = new List<GameObject>();
        _isShooting = new List<bool>();
        _linkedMB = entity.GetComponent<MonoBehaviour>();
    }

    public override void Prepare()
    {
        Team oppositeTeam = _entity.Team.CombatManager.GetOppositeTeam(_entity.Team);
        _entity.Team.RequestEntityTarget(new RequestEntityTargetDescriptor(oppositeTeam.Entities, OnTargetSelected));
    }

    public override void Execute()
    {
        _Orb = Instantiate(Resources.Load("MageCompetences/Orb"), _target.transform.position, Quaternion.identity) as GameObject;
        CreateMeteor();
        _targetPosition = _target.transform.position;
        _linkedMB.StartCoroutine(WaitForFall(_target));
    }

    private void CreateMeteor()
    {
        int randomNumberOfMeteor = Random.Range(5, 15);
        for (int i = 0; i < randomNumberOfMeteor; i++)
        {
            //Instantiate the meteor
            //Set the position aroud the Orb with a random offset
            _meteors.Add(Instantiate(Resources.Load("MageCompetences/Meteor"), _Orb.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity) as GameObject);
            //set the parent of the meteor to the target
            _meteors[i].transform.parent = _Orb.transform;
        }
    }

    private void MeteorsRotation()
    {
        //Rotate aroud the parent gameobject
        foreach (GameObject meteor in _meteors)
        {
            meteor.transform.RotateAround(_Orb.transform.position, Vector3.up, Random.Range(-10, 10) * Time.deltaTime);
        }
    }

    private void MeteorsFall(GameObject meteor, Entity target)
    {
        meteor.transform.parent = null;

        //Random direction Around the target
        Vector3 randomDirection = new Vector3(Random.Range(
            _targetPosition.x - 5, _targetPosition.x + 5), _targetPosition.y, Random.Range(_targetPosition.z - 5, _targetPosition.z + 5)
        );

        meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, randomDirection, 0.1f);
    }

    IEnumerator WaitForFall(Entity target)
    {
        foreach (GameObject meteor in _meteors)
        {   
            MeteorsRotation();
            yield return new WaitForSeconds(Random.Range(1, 3));
            MeteorsFall(meteor, target);
        }
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
