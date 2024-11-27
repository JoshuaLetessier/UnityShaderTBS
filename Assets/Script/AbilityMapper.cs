using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class AbilityMapper
{
    public static Dictionary<string, Func<PlayerTeam, CombatManager, bool>> map = new()
    {
        {"dummy", (PlayerTeam playerTeam, CombatManager combatManager)=>{new DummyAction(playerTeam, combatManager); return true; } },
    };
}
