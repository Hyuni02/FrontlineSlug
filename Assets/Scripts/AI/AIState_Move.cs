using UnityEngine;
public class AIState_Move : AIState {
    private Transform pos;
    private float dir;
    public AIState_Move(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() {
        SetDestination();
    }
    public override void Update() {
        if (!pos) return;
        
        //이동
        enemyAI.enemy.Move(dir);
        
        //목적지 도착
        if (Mathf.Abs(enemyAI.transform.position.x - pos.position.x) < .2f) {
            SetDestination();
        }
        
        //공격 대상 확인
        if (Vector2.Distance(enemyAI.player.transform.position, enemyAI.transform.position) < enemyAI.range) {
            enemyAI.ChangeState(EnemyAI.EnemyState.Attack);
        }
    }
    public override void Exit() { }

    private void SetDestination() {
        pos = enemyAI.GetComponent<MovePoint>().GetPos();

        dir = pos.position.x - enemyAI.transform.position.x;
        dir = dir > 0 ? 1 : -1;
    }
}
