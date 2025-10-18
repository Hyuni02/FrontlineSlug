using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rigid;

    private float hori;
    private float speed = 7;
    private float jumpPower = 800;
    
    private Vector2 vec_move;
    private Vector2 vec_jump;
    
    
    void Start() {
        rigid = GetComponent<Rigidbody2D>();

        vec_jump = new Vector2(0, jumpPower);
    }

    // Update is called once per frame
    void Update() {
        hori = Input.GetAxisRaw("Horizontal");
        vec_move = new Vector2(hori * speed, rigid.velocity.y);

        rigid.velocity = vec_move;

        if (Input.GetKeyDown(KeyCode.Space)) {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(vec_jump);
        }
    }
}
