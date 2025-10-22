public class EnemyAI_Drone : EnemyAI
{
    protected override void SelectState() {
        curr_state = EnemyState.Wait;
    }
}
