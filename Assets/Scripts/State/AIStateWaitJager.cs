using UnityEngine;
public class AIStateWaitJager : AIState
{
    public AIStateWaitJager(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() {
        enemyAI.enemy.Move(0);
    }
    public override void Update() {
        //공격 대상 확인
        GameObject player = PlayerController.instance.curDoll.gameObject;
        if (Vector2.Distance(player.transform.position, enemyAI.transform.position) < enemyAI.range) {
            enemyAI.ChangeState(EnemyAI.EnemyState.Attack);
        }
    }
    public override void Exit() {
    }
}
