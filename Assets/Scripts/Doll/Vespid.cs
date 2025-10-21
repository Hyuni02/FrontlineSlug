using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vespid : Enemy
{
    protected override void Start()
    {
        base.Start();

        speed = 3;
        dmg = 8;
        attackInterval = 4;
    }
}
