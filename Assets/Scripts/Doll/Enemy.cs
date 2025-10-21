using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Doll {
    public bool activate = false;

    protected override void Update() {
        if (!activate) return;
        base.Update();
    }


}
