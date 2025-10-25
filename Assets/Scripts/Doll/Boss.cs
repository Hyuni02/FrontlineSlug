using System;
using UnityEngine;
public class Boss : Enemy {
    public Sprite img_face;
    private int attackCounter = 2;
    public override void TryAttack(bool isPressed) {
        if (isPressed) {
            FlipModel((enemyAI.player.transform.position - transform.position).x < 0);
            while (attackCounter > 0) {
                Attack();
                attackCounter--;
            }
        }
        else {
            attackCounter = 2;
        }
    }

    protected override void Die() {
        enemyAI.ChangeState(EnemyAI.EnemyState.Die);
        base.Die();
    }
}
