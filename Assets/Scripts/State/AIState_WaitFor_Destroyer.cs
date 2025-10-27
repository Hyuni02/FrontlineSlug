using UnityEngine;

public class AIState_WaitFor_Destroyer : AIState {
    private float duration;
    private Transform target;

    public AIState_WaitFor_Destroyer(EnemyAI enemyAI) : base(enemyAI) { }

    public override void Enter() {
        Debug.Log("WaitFor");
        duration = 3;
        target = PlayerController.instance.curDoll.transform;
    }

    public override void Exit() { }

    public override void Update() {
        enemyAI.LookTarget(target);

        duration -= Time.deltaTime;

        if (duration < 0) {
            int act = Random.Range(0, 10);
            if (act >=0 && act < 4) {
                enemyAI.ChangeState(EnemyAI.EnemyState.Attack);
            }
            else if(act >= 4 && act < 8) {
                enemyAI.ChangeState(EnemyAI.EnemyState.Move);
            }
            else {
                enemyAI.ChangeState(EnemyAI.EnemyState.Skill);
            }
        }
    }
}
