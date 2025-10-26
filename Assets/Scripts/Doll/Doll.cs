using UnityEngine;
using Spine.Unity;

public class Doll : MonoBehaviour {
    public enum CharacterState {
        none, wait, move, attack, die, skill
    }

    //Components
    protected Rigidbody2D rigid;
    protected Animator animator;
    protected SkeletonMecanim mecanim;

    //Animator Paramter
    protected string para_move = "move";
    protected string para_attack = "attack";
    protected string para_die = "die";
    protected string para_attackPressed = "attackPressed";
    protected string para_attackCounter = "attackCounter";
    protected string para_victory = "victory";

    //Children
    protected Transform trans_muzzle;
    protected GroundChecker groundChecker;

    //State
    protected CharacterState prev_state = CharacterState.none;
    [SerializeField]
    protected CharacterState curr_state = CharacterState.wait;

    //variable
    protected Vector2 vec_move;
    protected Vector2 vec_jump;
    protected float speed = 7;
    protected int jumpPower = 800;
    protected float attackInterval = .5f;
    protected float intervalCounter = 0;
    protected float attakDuration = 0.5f;
    protected float durationCounter = 0;
    protected int deathDelay = 2;

    public GameObject pref_bullet;
    [HideInInspector]
    public int maxHP = 100;
    [SerializeField]
    public int currHP = 100;
    protected int dmg = 10;

    protected virtual void Awake() {
        //set component
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        mecanim = GetComponentInChildren<SkeletonMecanim>();
        groundChecker = GetComponentInChildren<GroundChecker>();

        //set variable
        vec_jump = new Vector2(0, jumpPower);
        trans_muzzle = transform.Find("muzzle");
    }

    protected virtual void Update() {
        if (prev_state == CharacterState.die) return;

        intervalCounter -= Time.deltaTime;
        durationCounter -= Time.deltaTime;
        animator.SetFloat(para_attackCounter, durationCounter);

        if (prev_state != curr_state) {
            prev_state = curr_state;
        }
    }

    public virtual void Move(float hori) {
        if (curr_state == CharacterState.die) return;
        if (durationCounter > 0) return;

        vec_move = new Vector2(hori * speed, rigid.velocity.y);
        rigid.velocity = vec_move;

        bool moving = hori != 0;

        if (moving) {
            FlipModel(hori < 0);
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        
        animator.SetBool(para_move, moving);
        curr_state = moving ? CharacterState.move : CharacterState.wait;
    }

    public virtual void Jump() {
        if (durationCounter > 0) return;
        if (!IsGrounded()) return;

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(vec_jump);
    }

    public virtual void TryAttack(bool isPressed) {
        animator.SetBool(para_attackPressed, isPressed);

        if (intervalCounter < 0 && isPressed) {
            Attack();
        }
    }

    protected virtual void Attack() {
        Move(0);
        intervalCounter = attackInterval;
        durationCounter = attakDuration;
        animator.SetTrigger(para_attack);
        curr_state = CharacterState.attack;
    }

    public virtual void Shoot() {
        GameObject obj = Instantiate(pref_bullet, trans_muzzle.position, Quaternion.identity);
        Vector2 dir = mecanim.skeleton.ScaleX > 0 ? Vector2.right : Vector2.left;
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, dmg, 24, dir));
    }

    public virtual void Hit(BulletData bulletData) {
        currHP -= bulletData.dmg;
        if (currHP <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        curr_state = CharacterState.die;
        vec_move = new Vector2(0, rigid.velocity.y);
        rigid.velocity = vec_move;
        animator.SetTrigger(para_die);
        gameObject.layer = LayerMask.NameToLayer("DeadBody");
    }

    public void GetEvent(string eventName) {
        switch (eventName) {
            case "fire":
                Shoot();
                break;
        }
    }

    protected void FlipModel(bool flip) {
        mecanim.skeleton.ScaleX = flip ? -1 : 1;
    }

    private bool IsGrounded() {
        return groundChecker.isGrounded;
    }
}
