using UnityEngine;

public class SAT8 : Friendly {
    Vector3 offset = new Vector3(0, 0.3f, 0);
    Vector2 offset2 = new Vector2(0, 0.1f);

    protected override void Awake() {
        base.Awake();

        speed = 4;
        dmg = 40;
        attackInterval = 1.5f;
        maxHP = 120;
        currHP = 120;
        range = 7;
    }

    protected override void Shoot() {
        GameObject obj = Instantiate(pref_bullet, trans_muzzle.position, Quaternion.identity);
        GameObject obj2 = Instantiate(pref_bullet, trans_muzzle.position, Quaternion.identity);
        GameObject obj3 = Instantiate(pref_bullet, trans_muzzle.position, Quaternion.identity);

        Vector2 dir = mecanim.skeleton.ScaleX > 0 ? Vector2.right : Vector2.left;
        Vector2 dir2 = mecanim.skeleton.ScaleX > 0 ? Vector2.right + offset2 : Vector2.left + offset2;
        Vector2 dir3 = mecanim.skeleton.ScaleX > 0 ? Vector2.right - offset2 : Vector2.left - offset2;

        if (target) {
            dir = (target.position - trans_muzzle.position).normalized;
            dir2 = (target.position - trans_muzzle.position + offset).normalized;
            dir3 = (target.position - trans_muzzle.position - offset).normalized;

            FlipModel((target.transform.position - transform.position).x < 0);
        }

        obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir));
        obj2.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir2));
        obj3.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir3));
    }
}
