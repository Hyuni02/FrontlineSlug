using UnityEngine;
public class AIState_Die : AIState {
    public AIState_Die(EnemyAI enemyAI) : base(enemyAI) { }
    public override void Enter() {
        Debug.Log($"Die : {enemyAI.gameObject.name}");
    }
    public override void Update() { }
    public override void Exit() { }
}
