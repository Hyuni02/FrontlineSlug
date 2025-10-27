using UnityEngine;
public class AIState_AttackOnce : AIState {
    private float duration = 3;

    public AIState_AttackOnce(EnemyAI enemyAI) : base(enemyAI) { }

    public override void Enter() {
        Debug.Log("AttackOnce");
        duration = 3;
        enemyAI.enemy.Attack();
    }
    public override void Update() {
        duration -= Time.deltaTime;
        if (duration < 0) {
            enemyAI.ChangeState(EnemyAI.EnemyState.Wait);
        }
    }

    public override void Exit() { }
}
