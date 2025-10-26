using UnityEngine;
public class AIState_Attack : AIState {
    public AIState_Attack(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() { }
    public override void Update() {
        enemyAI.enemy.TryAttack(true);
        
        //공격 대상 확인
        GameObject player = PlayerController.instance.curDoll.gameObject;
        if (Vector2.Distance(player.transform.position, enemyAI.transform.position) >= enemyAI.enemy.range) {
            enemyAI.ChangeState(EnemyAI.EnemyState.Move);
        }
    }
    public override void Exit() {
        enemyAI.enemy.TryAttack(false);
    }
}
