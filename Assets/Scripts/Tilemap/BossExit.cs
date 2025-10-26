using UnityEngine;

public class BossExit : MonoBehaviour {
    public bool bossDied = false;
    public void OnTriggerEnter2D(Collider2D other) {
        if (!bossDied) return;
        CameraController.instance.ExitBossRoom();
    }
}
