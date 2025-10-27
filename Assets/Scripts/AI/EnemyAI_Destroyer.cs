public class EnemyAI_Destroyer : EnemyAI {
    protected override void SetState() {
        curr_state = EnemyState.None;

        aiState_Wait = new AIState_WaitFor_Destroyer(this);
        aiState_Attack = new AIState_AttackOnce(this);
        aiState_Move = new AIState_MoveIgnore(this);
        aiState_Skill = new AIState_Skill_Destroyer(this);
        aiState_Die = new AIState_Die(this);
    }
    
    public override void Activate() {
        activate = true;
        ChangeState(EnemyState.Wait);
    }
}
