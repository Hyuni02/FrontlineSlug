using UnityEngine;

public class BossExit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        CameraController.instance.ExitBossRoom();
    }
}
