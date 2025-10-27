using System.Collections;
using UnityEngine;
public abstract class Friendly : Doll {
    public Sprite img_face;
    public Transform target;
    public Collider2D[] enemies;
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
            FlipModel((target.transform.position - transform.position).x < 0);
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

            //락온 이미지
            PlayerController.instance.targetArrow.SetActive(target);
            PlayerController.instance.crossHair.SetActive(target);
        
            if (target) {
                PlayerController.instance.crossHair.transform.position = target.position;
                var dir = target.position - PlayerController.instance.curDoll.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                PlayerController.instance.targetArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            PlayerController.instance.targetArrow.transform.position = PlayerController.instance.curDoll.transform.position;
        }
    }
}
