using UnityEngine;
public class BossTrigger : EnemyTrigger {
    public GameObject block;

    public override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        InGameUIController.instance.SetBossUI(lst_enemy[0].GetComponent<Boss>(), block);
    }
}
