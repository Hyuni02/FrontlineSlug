using UnityEngine;
public class Boss : Enemy {
    public Sprite img_face;
    private int attackCounter = 2;
    public BossTrigger trigger;
    public override void TryAttack(bool isPressed) {
        if (isPressed) {
            GameObject player = PlayerController.instance.curDoll.gameObject;
            FlipModel((player.transform.position - transform.position).x < 0);
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
        deathDelay = 5;
        base.Die();
        trigger.SetBlock(true);
        InGameUIController.instance.DisableBossUI();
        FindObjectOfType<BossExit>().bossDied = true;
    }
}
