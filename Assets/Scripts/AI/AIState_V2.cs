public abstract class AIState_V2
{
    protected EnemyAI_V2 enemyAI;
    public AIState_V2(EnemyAI_V2 enemyAI) {
        this.enemyAI = enemyAI;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
