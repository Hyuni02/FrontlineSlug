using System.Collections;
using UnityEngine;
public class Destroyer : Boss {
    //Children
    protected Transform trans_muzzle2;
    public Transform trans_rain;
    private bool skillUsed = false;

    protected override void Awake() {
        base.Awake();

        speed = 5;
        dmg = 30;
        attackInterval = 2;
        maxHP = 800;
        currHP = 800;

        trans_muzzle2 = transform.Find("muzzle2");
    }

    public override void GetEvent(string eventName) {
        switch (eventName) {
            case "fire":
                if (curr_state == CharacterState.skill) {
                    ShootHigh(trans_muzzle);
                }
                else {
                    Shoot();
                }
                break;
            case "fire2":
                if (curr_state == CharacterState.skill) {
                    ShootHigh(trans_muzzle2);
                }
                else {
                    Shoot2();
                }
                break;
        }
    }

    protected void Shoot2() {
        GameObject obj = Instantiate(pref_bullet, trans_muzzle2.position, Quaternion.identity);
        Vector2 dir = (PlayerController.instance.curDoll.transform.position - trans_muzzle2.position).normalized;
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir));
    }

    protected void ShootHigh(Transform muzzle) {
        GameObject obj = Instantiate(pref_bullet, muzzle.position, Quaternion.identity);
        Vector2 dir = Vector2.up;
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir));
        obj.GetComponent<Bullet>().rigid.gravityScale = 0;
        if (skillUsed == false) {
            skillUsed = true;
            StartCoroutine(cor_GrenadeRain());
        }
        Destroy(obj, 3f);
    }

    IEnumerator cor_GrenadeRain() {
        yield return new WaitForSeconds(2f);

        int i = Random.Range(0, 2);
        if (i == 0) i = 1;
        else i = -1;

        for (int j = -3; j < 4; j++) {
            Vector3 pos = trans_rain.position + new Vector3(i * j * 3, 0, 0);
            GameObject obj = Instantiate(pref_bullet, pos, Quaternion.identity);
            Vector2 dir = Vector2.down;
            obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 0, dir));
            yield return new WaitForSeconds(.7f);
        }
        skillUsed = false;
    }
}
