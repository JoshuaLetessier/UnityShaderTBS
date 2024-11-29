using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer
{
    public struct Link
    {
        public Action func;
        public float duration;
    }

    List<Link> _sequence;
    int _currentLinkID;
    MonoBehaviour _linkedMB;
    bool _isRunning;



    public void Init(List<Link> sequence, MonoBehaviour linkedMB)
    {
        _sequence = sequence;
        _currentLinkID = 0;
        _linkedMB = linkedMB;
        _isRunning = false;
    }

    public void StartSequence()
    {
        _isRunning = true;
        _currentLinkID = 0;
        NextLink();
    }

    void NextLink()
    {
        _sequence[_currentLinkID].func();

        if(_currentLinkID == _sequence.Count -1)
        {
            _isRunning = false;
            return;
        }

        _linkedMB.StartCoroutine(WaitForNextLink(_sequence[_currentLinkID].duration));
        ++_currentLinkID;
    }

    IEnumerator WaitForNextLink(float duration)
    {
        yield return new WaitForSeconds(duration);
        NextLink();
    }
}
