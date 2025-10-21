using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState_Chase : AIState {
    public AIState_Chase(EnemyAI enemyAI) : base(enemyAI) {
    }

    public override void Enter() {
    }

    public override void Exit() {
        enemyAI.enemy.Move(0);
    }

    public override void Update() {
        Vector2 dir = (enemyAI.player.transform.position - enemyAI.transform.position).normalized;
        enemyAI.enemy.Move(dir.x);
    }
}
