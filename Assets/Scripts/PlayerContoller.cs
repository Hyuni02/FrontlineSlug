using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour {
    public Player player;

    private float hori;
    private void Update() {
        hori = Input.GetAxisRaw("Horizontal");
        player.Move(hori);
        
        if (Input.GetKeyDown(KeyCode.X)) {
            player.Jump();
        }

        if (Input.GetKey(KeyCode.Z)) {
            player.Attack();
        }
    }
}
