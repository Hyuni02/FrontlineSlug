using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V2 : MonoBehaviour
{
    public static PlayerController_V2 instance;

    public Friendly_V2 player;

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
    }
}
