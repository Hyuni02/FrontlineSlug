public abstract class AIState
{
    protected EnemyAI enemyAI;
    public AIState(EnemyAI enemyAI) {
        this.enemyAI = enemyAI;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
