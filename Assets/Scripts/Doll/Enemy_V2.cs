using UnityEngine;
public class Enemy_V2 : Doll_V2 {
    protected EnemyAI_V2 enemyAI;

    protected override void Awake() {
        base.Awake();

        enemyAI = GetComponent<EnemyAI_V2>();
    }
    
    public override void TryAttack(bool isPressed) {
        animator.SetBool(para_attackPressed, isPressed);

        if (isPressed) {
            FlipModel((enemyAI.player.transform.position - transform.position).x < 0);
            if (intervalCounter < 0) {
                Attack();
            }
        }
    }
    
    protected override void Die() {
        enemyAI.ChangeState(EnemyAI_V2.EnemyState.Die);
        base.Die();
        Destroy(gameObject, deathDelay);
    }
}
