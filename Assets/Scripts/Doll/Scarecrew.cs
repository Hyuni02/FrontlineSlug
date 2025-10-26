using UnityEngine;
public class Scarecrew : Boss
{
    //Children
    protected Transform trans_muzzle2;
    protected Transform trans_muzzle3;

    protected override void Awake() {
        base.Awake();
        
        speed = 7;
        dmg = 20;
        attackInterval = 2;
        maxHP = 400;
        currHP = 400;
        
        trans_muzzle2 = transform.Find("muzzle2");
        trans_muzzle3 = transform.Find("muzzle3");
    }
    
    protected override void Update() {
        if (prev_state == CharacterState.die) return;

        if (prev_state != curr_state) {
            prev_state = curr_state;
        }
    }
}
