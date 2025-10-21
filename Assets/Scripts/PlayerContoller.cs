using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour {
    public Friendly player;

    private float hori;
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
            foreach(var enemy in FindObjectsOfType<Enemy>()) {
                enemy.activate = true;
            }
        }
    }
}
