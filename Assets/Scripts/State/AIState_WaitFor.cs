using UnityEngine;
public class AIState_WaitFor : AIState {
    private float duration;
    private Transform target;
    public AIState_WaitFor(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() {
        Debug.Log("WaitFor");
        duration = 3;
        target = PlayerController.instance.player.transform;
    }
    public override void Update() {
        enemyAI.LookTarget(target);
        
        duration -= Time.deltaTime;

        if (duration < 0) {
            int act = Random.Range(0, 2);
            if (act == 0) {
                enemyAI.ChangeState(EnemyAI.EnemyState.Attack);
            }
            else {
                enemyAI.ChangeState(EnemyAI.EnemyState.Move);
            }
        }
    }
    public override void Exit() { }
}
