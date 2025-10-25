using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Friendly player;

    [HideInInspector]
    public float hori;

    private void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update() {
        hori = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.Z)) {
            player.TryAttack(true);
        }
        else {
            player.TryAttack(false);
            player.Move(hori);

            if (Input.GetKeyDown(KeyCode.X)) {
                player.Jump();
            }
        }
        
        //temp
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (var enemy in FindObjectsOfType<EnemyAI>()) {
                enemy.Activate();
            }
        }
    }
}
