using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour {
    public Kar98k player;

    private float hori;
    private void Update() {
        hori = Input.GetAxisRaw("Horizontal");
        player.Move(hori);
        
        if (Input.GetKeyDown(KeyCode.X)) {
            player.Jump();
        }

        player.TryAttack(Input.GetKey(KeyCode.Z));
    }
}
