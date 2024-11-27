using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class AbilityMapper
{
    public static Dictionary<string, Func<Competence>> map = new()
    {
        {"OrbMeteor", ()=>{return new OrbMeteorCompetence(); } },
    };
}
