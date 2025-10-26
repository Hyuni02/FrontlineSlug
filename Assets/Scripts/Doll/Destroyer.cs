using UnityEngine;
public class Destroyer : Boss {
    //Children
    protected Transform trans_muzzle2;
    protected override void Awake() {
        base.Awake();
        
        speed = 5;
        dmg = 30;
        attackInterval = 2;
        maxHP = 800;
        currHP = 800;
        
        trans_muzzle2 = transform.Find("muzzle2");
    }
}
