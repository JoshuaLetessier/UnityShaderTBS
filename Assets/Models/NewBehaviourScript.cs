using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] UnityEvent uEvent;
    [SerializeField] float _range = 2f;
    [SerializeField] float _speed = 5;

    [SerializeField] Transform mechant;
    Vector3 basePos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoTo(mechant.position);
            basePos = transform.position;
            animator.SetTrigger("Slash");
        }
    }

    public void OnSlash()
    {
        uEvent.Invoke();
    }

    public void GoTo(Vector3 target)
    {
        
        StartCoroutine(GoToCoroutine(target));
    }

    IEnumerator GoToCoroutine(Vector3 target)
    {
        
        float dist = 100;

        while(dist > _range)
        {
            Vector3 distVect = target - transform.position;
            distVect.y = 0;
            dist = distVect.magnitude;
            transform.position += Vector3.Normalize(distVect) * Time.deltaTime * _speed;
            yield return null;
        }
        animator.SetTrigger("AtDestination");
    }

    public void BackToOriginalPos()
    {
        GoTo(basePos);
    }
}
