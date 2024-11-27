using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class OrbMeteorCompetence : Competence
{

    [SerializeField] private int _damage;
    [SerializeField] private int _cost;
    [SerializeField] private int _cooldown;

     private GameObject _Orb;

    private List<GameObject> _meteors;
    private List<bool> _isShooting;

    MonoBehaviour _linkedMB;

    Vector3 _targetPosition;

    private void Start()
    {
        Init("OrbMeteor", _cost, _damage, _cooldown);
    }

    public override void Apply(Entity target)
    {
        _Orb = Instantiate(Resources.Load("MageCompetences/Orb"), target.transform.position, Quaternion.identity) as GameObject;
        CreateMeteor();
        _targetPosition = target.transform.position;
        _linkedMB.StartCoroutine(WaitForFall(target));
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
    }
}
