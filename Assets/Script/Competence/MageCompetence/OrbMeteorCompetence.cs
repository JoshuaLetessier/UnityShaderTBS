using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class OrbMeteorCompetence : Competence
{
    public override string Name { get => "OrbMeteor"; }
    public override int Cost { get => 0; }
    public override int Damage { get => 10; }
    public override int Cooldown { get => 0; }

    [SerializeField] GameObject _OrbPrefab;
    [SerializeField] GameObject _MeteorPrefab;
    [SerializeField] CinemachineVirtualCamera _camera;

    private GameObject _Orb;
    public List<GameObject> _Meteors;

    Vector3 _targetPosition;

    private Entity _target;
    private int _nbMeteor; 

    public OrbMeteorCompetence(Entity entity) : base(entity)
    {
        _Meteors = new List<GameObject>();
    }

    public override void Prepare()
    {
        Team oppositeTeam = _entity.Team.CombatManager.GetOppositeTeam(_entity.Team);
        _entity.Team.RequestEntityTarget(new RequestEntityTargetDescriptor(oppositeTeam.Entities, OnTargetSelected));
    }

    public override void Execute()
    {
        _Orb = Instantiate(_OrbPrefab, new Vector3(0,2,0), Quaternion.identity);
        //create a camera to follow the orb
        _camera = Instantiate(_camera, new Vector3(0, 0, 0), Quaternion.identity);
      
        _camera.Priority = 30;
        //_camera.transform.LookAt(_Orb.transform);
        _camera.LookAt = _Orb.transform;
        _camera.Follow = _Orb.transform;
        _camera.transform.position = new Vector3(_Orb.transform.position.x, _Orb.transform.position.y + 25, _Orb.transform.position.z - 10);
        _camera.transform.rotation = Quaternion.Euler(45, 0, 0);


        StartCoroutine(WaitAnimation());
        _Orb.transform.GetComponent<Animator>().SetBool("StopCast", true);
        //_Orb.transform.GetComponent<Animator>().enabled = false;
        CreateMeteor();

        _targetPosition = _target.transform.position;
        StartCoroutine(WaitForRotation(_target));
    }

    IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(1.3f);
        _Orb.transform.GetComponent<Animator>().enabled = false;
    }

    private void CreateMeteor()
    {
        // Vérifie les références nécessaires
        if (_MeteorPrefab == null || _Orb == null)
        {
            Debug.LogError("MeteorPrefab or Orb is null");
            return;
        }

        // Initialise les météores s'ils ne le sont pas déjà
        if (_Meteors == null)
            _Meteors = new List<GameObject>();

        int randomNumberOfMeteor = Random.Range(5, 15);
        _nbMeteor = randomNumberOfMeteor;
        for (int i = 0; i < randomNumberOfMeteor; i++)
        {
            // Génère une position aléatoire autour de l'orb
            Vector3 randomOffset;
            do
            {
                randomOffset = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
            } while (randomOffset.magnitude < 1.0f); // Évite une proximité trop proche

            // Instancie et ajoute le météore à la liste
            GameObject newMeteor = Instantiate(_MeteorPrefab, _Orb.transform.position + randomOffset, Quaternion.identity);
            newMeteor.transform.parent = _Orb.transform;
            _Meteors.Add(newMeteor);
        }

        Debug.Log($"{randomNumberOfMeteor} meteors created successfully.");
    }


    IEnumerator WaitForRotation(Entity target)
    {
        float duration = 3f; 
        float elapsedTime = 0f; 

 
        while (elapsedTime < duration)
        {
            MeteorsRotation(); 
            elapsedTime += Time.deltaTime; 
            yield return null; 
        }

        StartCoroutine(WaitForFall(_target));
    }

    private void MeteorsRotation()
    {
        //Rotate aroud the parent gameobject
        foreach (GameObject meteor in _Meteors)
        {
            if(meteor != null)
                meteor.transform.RotateAround(_Orb.transform.position, Vector3.up, 100 * Time.deltaTime);
        }
    }

    IEnumerator WaitForFall(Entity target)
    {
        float duration = 2f;
        float elapsedTime = 0f;


        while (elapsedTime < duration)
        {
            MeteorsFall(target);
           
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(_Orb);


        target.TakeDamage(Damage * _nbMeteor);

        _camera.Priority = -1;

        Exit();
    }

    private void MeteorsFall(Entity target)
    {
        foreach (GameObject meteor in _Meteors)
        {
            if (meteor == null)
                continue;
            meteor.transform.parent = null;

            //Random direction Around the target
            Vector3 randomDirection = new Vector3(Random.Range(
                _targetPosition.x - 2, _targetPosition.x + 2), _targetPosition.y, Random.Range(_targetPosition.z - 2, _targetPosition.z + 2)
            );
            meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, _targetPosition, 0.1f);
        }
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