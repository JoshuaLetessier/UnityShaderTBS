using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction
{
    abstract public void Prepare();

    abstract public void Execute();

    abstract public void Exit();
}
