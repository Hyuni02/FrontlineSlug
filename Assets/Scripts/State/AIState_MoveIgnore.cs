using UnityEngine;
public class AIState_MoveIgnore : AIState {
    private Transform pos;
    private float dir;
    public AIState_MoveIgnore(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() {
        Debug.Log("MoveIgnore");
        SetDestination();
    }
    public override void Update() {
        if (!pos) return;
        
        //이동
        enemyAI.enemy.Move(dir);
        
        //목적지 도착
        if (Mathf.Abs(enemyAI.transform.position.x - pos.position.x) < .2f) {
            Debug.Log("arrived");
            enemyAI.ChangeState(EnemyAI.EnemyState.Wait);
        }
    }
    public override void Exit() {
        enemyAI.enemy.Move(0);
    }
    
    private void SetDestination() {
        pos = enemyAI.GetComponent<MovePoint>().GetPos();
        dir = pos.position.x - enemyAI.transform.position.x;
        dir = dir > 0 ? 1 : -1;
    }
}
