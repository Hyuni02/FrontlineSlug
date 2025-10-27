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

    private void Update() {
        Vector2 velocity = rigid.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        var targetRot = Quaternion.Euler(0, 0, angle - 90);
        obj.rotation = Quaternion.Slerp(obj.rotation, targetRot, Time.deltaTime * 10);
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