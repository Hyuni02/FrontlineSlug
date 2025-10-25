public class EnemyAI_Jager_V2 : EnemyAI_V2 {
    protected override void SetState() {
        base.SetState();

        aiState_Wait = new AIState_Wait_Jager_V2(this);
        aiState_Attack = new AIState_Attack_Jager_V2(this);
    }

    public override void Activate() {
        activate = true;
        ChangeState(EnemyState.Wait);
    }
}
