using UnityEngine;

public class Friendly_V2 : Doll_V2
{
    protected override void Die(int delay = 2) {
        vec_move = new Vector2(0, rigid.velocity.y);
        rigid.velocity = vec_move;
        Destroy(gameObject);
    }
}
