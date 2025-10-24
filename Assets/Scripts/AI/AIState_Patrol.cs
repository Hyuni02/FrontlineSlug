using UnityEngine;
public class AIState_Patrol : AIState {
    public AIState_Patrol(EnemyAI enemyAI) : base(enemyAI) { }

    private bool prev;
    private bool curr;
    private Vector2 dir = Vector2.left;
    public override void Enter() {Debug.Log("patrol start"); }
    public override void Update() {
        if (enemyAI.enemy.TouchWall()) {
            dir.x *= -1;
        }
        enemyAI.enemy.Move(dir.x);
    }
    public override void Exit() { }
}
