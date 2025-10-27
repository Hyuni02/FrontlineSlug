using UnityEngine;

public class Bullet_Penetration : Bullet
{
    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == data.from.layer) return;

        other.GetComponent<Hitable>()?.Hit(data);
        HitEffect();
        if (other.gameObject.layer == LayerMask.NameToLayer("Tilemap")) {
            Destroy(gameObject);
        }
    }
}
