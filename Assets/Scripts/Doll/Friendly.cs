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

        Transform closestEnemy = null;
        float minSqrDistance = float.MaxValue;

        foreach (var enemyCollider in enemies)
        {
            float sqrDistance = ((Vector2)(enemyCollider.transform.position - transform.position)).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                closestEnemy = enemyCollider.transform;
            }
        }

        target = closestEnemy;

        //락온 이미지
        PlayerController.instance.SetCrossHair(target);
    }
}
