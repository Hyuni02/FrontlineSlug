using UnityEngine;
public class AIState_Attack_Jager_V2 : AIState_V2 {
    public AIState_Attack_Jager_V2(EnemyAI_V2 enemyAI) : base(enemyAI) { }
    public override void Enter() { }
    public override void Update() {
        enemyAI.enemy.TryAttack(true);

        //공격 대상 확인
        if (Vector2.Distance(enemyAI.player.transform.position, enemyAI.transform.position) >= enemyAI.range) {
            enemyAI.ChangeState(EnemyAI_V2.EnemyState.Wait);
        }
    }
    public override void Exit() {
        enemyAI.enemy.TryAttack(false);
    }
}
