using UnityEngine;
public class Enemy : Doll {
    protected EnemyAI enemyAI;

    protected override void Awake() {
        base.Awake();

        enemyAI = GetComponent<EnemyAI>();
    }
    
    public override void TryAttack(bool isPressed) {
        GameObject player = PlayerController.instance.curDoll.gameObject;
        animator.SetBool(para_attackPressed, isPressed);

        if (isPressed) {
            FlipModel((player.transform.position - transform.position).x < 0);
            if (intervalCounter < 0) {
                Attack();
            }
        }
    }
    
    protected override void Die() {
        enemyAI.ChangeState(EnemyAI.EnemyState.Die);
        base.Die();
        Destroy(gameObject, deathDelay);
    }
}
