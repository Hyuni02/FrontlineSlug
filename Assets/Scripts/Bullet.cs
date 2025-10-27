using UnityEngine;

public class BulletData {
    public GameObject from;
    public int dmg;
    public float speed;
    public Vector2 dir;

    public BulletData(GameObject from, int dmg, float speed, Vector2 dir) {
        this.from = from;
        this.dmg = dmg;
        this.speed = speed;
        this.dir = dir;
    }
}

public class Bullet : MonoBehaviour {
    protected BulletData data;
    protected Rigidbody2D rigid;

    protected virtual void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void init(BulletData bulletData) {
        data = bulletData;
        rigid.velocity = data.dir * data.speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == data.from.layer) return;

        other.GetComponent<Hitable>()?.Hit(data);
        HitEffect();
        Destroy(gameObject);
    }

    protected virtual void HitEffect() {

    }
}
