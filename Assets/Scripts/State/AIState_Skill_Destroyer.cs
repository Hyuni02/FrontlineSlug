using UnityEngine;

public class AIState_Skill_Destroyer : AIState {
    private float duration = 5.0f;
    private Transform target;

    public AIState_Skill_Destroyer(EnemyAI enemyAI) : base(enemyAI) { }

    public override void Enter() {
        Debug.Log("Skill");
        duration = 5;
        target = PlayerController.instance.curDoll.transform;
        enemyAI.enemy.Skill();
    }

    public override void Exit() { }

    public override void Update() {
        enemyAI.LookTarget(target);

        duration -= Time.deltaTime;

        if (duration < 0) {
            enemyAI.ChangeState(EnemyAI.EnemyState.Wait);
        }
    }
}
