public class EnemyAI_Drone_V2 : EnemyAI_V2
{
    public override void Activate() {
        activate = true;
        ChangeState(EnemyState.Wait);
    }
}
