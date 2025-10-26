using System.Collections;
using UnityEngine;
public abstract class Friendly : Doll {
    public Sprite img_face;
    public Transform target;
    private Collider2D[] enemies;
    protected override void Die() {
        base.Die();
        InGameManager.instance.DollDie();
        StartCoroutine(cor_RemoveBody());
    }

    IEnumerator cor_RemoveBody() {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
    
    protected override void Shoot() {
        GameObject obj = Instantiate(pref_bullet, trans_muzzle.position, Quaternion.identity);
        Vector2 dir = mecanim.skeleton.ScaleX > 0 ? Vector2.right : Vector2.left;
        if (target) {
            dir = (target.position - trans_muzzle.position).normalized;
        }
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir));
    }

    private void LateUpdate() {
        enemies = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));

        if (enemies.Length == 0) target = null;
        
        for (int i = 0; i < enemies.Length; i++) {
            if (Physics2D.Raycast(transform.position, enemies[i].transform.position)) {
                if (!target) {
                    target = enemies[i].transform;
                }
                else {
                    var dis_target = Vector2.Distance(transform.position, target.position);
                    var dis_new = Vector2.Distance(transform.position, enemies[i].transform.position);
                    if (dis_target > dis_new) {
                        target = enemies[i].transform;
                    }
                }
            }
        }
        
        //락온 이미지
        PlayerController.instance.SetCrossHair(target);
    }
}
