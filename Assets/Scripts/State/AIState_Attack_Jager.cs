using UnityEngine;
public class AIState_Attack_Jager : AIState {
    public AIState_Attack_Jager(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() { }
    public override void Update() {
        GameObject player = PlayerController.instance.curDoll.gameObject;
        enemyAI.enemy.TryAttack(true);

        //공격 대상 확인
        if (Vector2.Distance(player.transform.position, enemyAI.transform.position) >= enemyAI.range) {
            enemyAI.ChangeState(EnemyAI.EnemyState.Wait);
        }
    }
    public override void Exit() {
        enemyAI.enemy.TryAttack(false);
    }
}
