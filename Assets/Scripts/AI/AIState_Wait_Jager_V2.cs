using UnityEngine;
public class AIState_Wait_Jager_V2 : AIState_V2
{
    public AIState_Wait_Jager_V2(EnemyAI_V2 enemyAI) : base(enemyAI) { }
    public override void Enter() {
        enemyAI.enemy.Move(0);
    }
    public override void Update() {
        //공격 대상 확인
        if (Vector2.Distance(enemyAI.player.transform.position, enemyAI.transform.position) < enemyAI.range) {
            enemyAI.ChangeState(EnemyAI_V2.EnemyState.Attack);
        }
    }
    public override void Exit() {
    }
}
