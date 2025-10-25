public class AIState_Wait : AIState {
    public AIState_Wait(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() {
        enemyAI.enemy.Move(0);
    }
    public override void Update() { }
    public override void Exit() { }
}
