using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompetenceTest : Competence
{
    private void Start()
    {
        Init("Test competence", 30, 10, 10);
    }

    public override void Apply(Entity target)
    {
        Debug.Log("Test competence applied");
    }
}
