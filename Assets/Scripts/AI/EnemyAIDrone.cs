public class EnemyAIDrone : EnemyAI
{
    public override void Activate() {
        activate = true;
        ChangeState(EnemyState.Wait);
    }
}
