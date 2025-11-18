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
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, status.dmg, 24, dir));
    }

    private void LateUpdate() {
        AutoAim();
    }

    private void AutoAim() {
        enemies = Physics2D.OverlapCircleAll(transform.position, status.range, LayerMask.GetMask("Enemy"));

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider2D enemyCollider in enemies) {
            Vector2 directionToEnemy = enemyCollider.transform.position - transform.position;
            float dSqrToEnemy = directionToEnemy.sqrMagnitude;

            // 현재까지 찾은 가장 가까운 적보다 멀리 있다면 더 이상 확인할 필요 없음
            if (dSqrToEnemy > closestDistanceSqr) {
                continue;
            }

            // 장애물(Tilemap) 확인
            if (!Physics2D.Raycast(transform.position, directionToEnemy.normalized, directionToEnemy.magnitude, LayerMask.GetMask("Tilemap"))) {
                // 장애물이 없다면, 이 적을 가장 가까운 타겟으로 설정
                closestDistanceSqr = dSqrToEnemy;
                bestTarget = enemyCollider.transform;
            }
        }

        target = bestTarget;

        //락온 이미지
        PlayerController.instance.SetCrossHair(this, target);
    }
}
