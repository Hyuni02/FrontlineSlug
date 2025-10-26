using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : EnemyTrigger {
    public List<GameObject> lst_block;
    public Transform pivot;
    public override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        
        CameraController.instance.EnterBossRoom(pivot, 7);
        
        SetBlock(false);
        InGameUIController.instance.SetBossUI(lst_enemy[0].GetComponent<Boss>());
    }

    public void SetBlock(bool set) {
        foreach (var block in lst_block) {
            block.GetComponent<BoxCollider2D>().isTrigger = set;
        }
    }
}
