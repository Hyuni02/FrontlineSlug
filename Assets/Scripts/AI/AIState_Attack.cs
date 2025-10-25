public class AIState_Attack : AIState {
    public AIState_Attack(EnemyAI enemyAI) : base(enemyAI) {
    }

    public override void Enter() {
    }

    public override void Exit() {
        enemyAI.enemy.TryAttack(false);
    }

    public override void Update() {
        enemyAI.enemy.FlipModel((enemyAI.player.transform.position - enemyAI.transform.position).x < 0);
        enemyAI.enemy.TryAttack(true);
    }
}
