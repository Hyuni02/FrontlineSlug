using UnityEngine;
public class AIState_Attack : AIState {
    public AIState_Attack(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() { }
    public override void Update() {
        enemyAI.enemy.TryAttack(true);
        
        //공격 대상 확인
        if (Vector2.Distance(enemyAI.player.transform.position, enemyAI.transform.position) >= enemyAI.range) {
            enemyAI.ChangeState(EnemyAI.EnemyState.Move);
        }
    }
    public override void Exit() {
        enemyAI.enemy.TryAttack(false);
    }
}
