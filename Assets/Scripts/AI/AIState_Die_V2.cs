using UnityEngine;
public class AIState_Die_V2 : AIState_V2 {
    public AIState_Die_V2(EnemyAI_V2 enemyAI) : base(enemyAI) { }
    public override void Enter() {
        Debug.Log($"Die : {enemyAI.gameObject.name}");
    }
    public override void Update() { }
    public override void Exit() { }
}
