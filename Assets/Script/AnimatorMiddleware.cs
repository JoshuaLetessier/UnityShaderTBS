using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMiddleware : MonoBehaviour
{
    [SerializeField] GameObject _entity;

    public void BackToPosition()
    {
        print("triggered");
        _entity.GetComponent<Slash>().BackToOriginalPos();
    }

    public void TriggerSlash()
    {
        _entity.GetComponent<Slash>().TriggerSlash();
    }

    public void TriggerThunder()
    {
        _entity.GetComponent<Battlecry>().TriggerThunder();
    }

    public void TriggerEstoc()
    {
        _entity.GetComponent<Slash>().TriggerEstoc();
    }
}
