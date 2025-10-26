using UnityEngine;
public class Scarecrew : Boss {
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
        range = 30;

        trans_muzzle2 = transform.Find("muzzle2");
        trans_muzzle3 = transform.Find("muzzle3");
    }

    protected override void Update() {
        if (prev_state == CharacterState.die) return;

        if (prev_state != curr_state) {
            prev_state = curr_state;
        }
    }

    public override void Attack() {
        animator.SetTrigger(para_attack);
        curr_state = CharacterState.attack;
    }

    public override void GetEvent(string eventName) {
        switch (eventName) {
            case "fire":
                Shoot();
                break;
            case "fire2":
                Shoot2();
                break;
            case "fire3":
                Shoot3();
                break;
        }
    }
    
    protected void Shoot2() {
        GameObject obj = Instantiate(pref_bullet, trans_muzzle2.position, Quaternion.identity);
        Vector2 dir = (PlayerController.instance.curDoll.transform.position - trans_muzzle2.position).normalized;
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir));
    }
    protected void Shoot3() {
        GameObject obj = Instantiate(pref_bullet, trans_muzzle3.position, Quaternion.identity);
        Vector2 dir = (PlayerController.instance.curDoll.transform.position - trans_muzzle3.position).normalized;
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir));
    }
}
