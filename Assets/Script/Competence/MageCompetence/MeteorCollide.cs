using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MeteorCollide : MonoBehaviour
{

    OrbMeteorCompetence _orbMeteorCompetence;
    VisualEffect _visualEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Entity")
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(_orbMeteorCompetence.Damage);
            Destroy(gameObject);
        }
    }

    //private void OnDestroyGameObject()
    //{
    //    Gradient gradient = new Gradient();
    //    Color color = gradient.Evaluate(0.0f);

    //    //_visualEffect.SetGradient("BrightColor", new Gradient());
    //}
}
