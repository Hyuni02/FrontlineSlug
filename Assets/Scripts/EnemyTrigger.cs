using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {
    public List<EnemyAI> lst_enemy;

    public void OnTriggerEnter2D(Collider2D other) {
        foreach (var enemy in lst_enemy) {
            enemy.activate = true;
        }
    }
}
