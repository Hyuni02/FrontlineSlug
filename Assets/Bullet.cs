using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData {
    public GameObject from;
    public LayerMask targetLayer;
    public int dmg;
    public float speed;
    public Vector2 dir;

    public BulletData(GameObject from, LayerMask targetLayer, int dmg, float speed, Vector2 dir) {
        this.from = from;
        this.targetLayer = targetLayer;
        this.dmg = dmg;
        this.speed = speed;
        this.dir = dir;
    }
}

public class Bullet : MonoBehaviour {
    private BulletData data;
    private Rigidbody2D rigid;
    
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    public void init(BulletData bulletData) {
        data = bulletData;
        rigid.velocity = data.dir * data.speed;
    }

    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.layer == data.targetLayer) {
            Destroy(gameObject);
        }
    }
}
