using UnityEngine;

public class Grenade : Bullet {
    private Transform obj;
    public float range;

    protected override void Awake() {
        base.Awake();
        obj = transform.GetChild(0);
    }

    public override void init(BulletData bulletData) {
        base.init(bulletData);
        float distance = Vector2.Distance(bulletData.from.transform.position, PlayerController.instance.curDoll.transform.position);
        rigid.AddForce(Vector2.up * distance, ForceMode2D.Impulse);
    }

    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == data.from.layer) return;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Player"));
        foreach (var collider in collider2Ds) {
            collider.GetComponent<Hitable>()?.Hit(data);
        }
        HitEffect();
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}