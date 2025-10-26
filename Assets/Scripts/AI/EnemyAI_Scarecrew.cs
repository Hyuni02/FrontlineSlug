public class EnemyAI_Scarecrew : EnemyAI {
    protected override void SetState() {
        curr_state = EnemyState.None;
        
        aiState_Wait = new AIState_WaitFor(this);
        aiState_Attack = new AIState_AttackOnce(this);
        aiState_Move = new AIState_MoveIgnore(this);
        aiState_Die = new AIState_Die(this);
    }

    public override void Activate() {
        activate = true;
        ChangeState(EnemyState.Wait);
    }
}
