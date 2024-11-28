using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MeteorCollide : MonoBehaviour
{
    VisualEffect _visualEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(WaitExplosion());
        }
    }

    IEnumerator WaitExplosion()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }


    //private void OnDestroyGameObject()
    //{
    //    Gradient gradient = new Gradient();
    //    Color color = gradient.Evaluate(0.0f);

    //    //_visualEffect.SetGradient("BrightColor", new Gradient());
    //}
}
