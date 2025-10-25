using UnityEngine;
public class AIState_WaitFor : AIState {
    private float duration;
    public AIState_WaitFor(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() {
        Debug.Log("WaitFor");
        duration = 3;
    }
    public override void Update() {
        duration -= Time.deltaTime;

        if (duration < 0) {
            int act = Random.Range(0, 2);
            if (act == 0) {
                //attack
                enemyAI.ChangeState(EnemyAI.EnemyState.Attack);
            }
            else {
                //move
                enemyAI.ChangeState(EnemyAI.EnemyState.Move);
            }
        }
    }
    public override void Exit() { }
}
