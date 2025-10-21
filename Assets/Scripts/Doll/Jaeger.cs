using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaeger : Enemy {
    protected override void Start() {
        base.Start();

        speed = 1.2f;
        dmg = 30;
        attackInterval = 8;
    }
}
