using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMiddleware : MonoBehaviour
{
    [SerializeField] GameObject _entity;
    [SerializeField] Animator _bookAnimator;

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

    public void OpenBook()
    {
        _bookAnimator.SetTrigger("Open");
    }

    public void CloseBook()
    {
        _bookAnimator.SetTrigger("Close");
    }

    public void SpawnBow()
    {
        _entity.GetComponent<MagicBow>().SpawnBow();
    }

    public void DespawnBow()
    {
        _entity.GetComponent<MagicBow>().DespawnBow();
    }
    
    public void SpawnArrow()
    {
        _entity.GetComponent<MagicBow>().SpawnArrow();
    }
}
