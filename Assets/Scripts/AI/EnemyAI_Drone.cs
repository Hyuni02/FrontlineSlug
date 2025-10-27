public class EnemyAI_Drone : EnemyAI
{
    public override void Activate() {
        activate = true;
        ChangeState(EnemyState.Wait);
    }
}
