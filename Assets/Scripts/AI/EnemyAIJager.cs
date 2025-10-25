public class EnemyAIJager : EnemyAI {
    protected override void SetState() {
        base.SetState();

        aiState_Wait = new AIStateWaitJager(this);
        aiState_Attack = new AIState_Attack_Jager(this);
    }

    public override void Activate() {
        activate = true;
        ChangeState(EnemyState.Wait);
    }
}
