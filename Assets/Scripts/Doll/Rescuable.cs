using System;
using UnityEngine;

public class Rescuable : MonoBehaviour {
    public bool rescued;
    private Friendly doll;
    private float dir;
    private void Start() {
        doll = GetComponent<Friendly>();
    }

    private void Update() {
        if (!rescued) return;
        //거리가 멀어지면 플레이어쪽으로 따라가기
        if (Vector2.Distance(transform.position, PlayerController.instance.player.transform.position) > 2) {
            if (transform.transform.position.x - PlayerController.instance.player.transform.position.x > 0) {
                dir = -1;
            }
            else {
                dir = 1;
            }
            doll.Move(dir);
        }
        else {
            doll.Move(0);
        }
    }
}
