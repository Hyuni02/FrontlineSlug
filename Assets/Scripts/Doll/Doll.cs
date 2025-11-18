using UnityEngine;
using Spine.Unity;

public abstract class Doll : MonoBehaviour {
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
    protected string para_skill = "skill";
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
    public DollStatus status;   
    protected int jumpPower = 16;
    protected float intervalCounter = 0;
    protected float durationCounter = 0;
    protected int deathDelay = 2;

    public GameObject pref_bullet;
    [SerializeField]
    public int curHP;

    protected virtual void Awake() {
        //set component
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        mecanim = GetComponentInChildren<SkeletonMecanim>();
        groundChecker = GetComponentInChildren<GroundChecker>();

        //set variable
        vec_jump = new Vector2(0, jumpPower);
        trans_muzzle = transform.Find("muzzle");
        curHP = status.maxHP;
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

        vec_move = new Vector2(hori * status.speed, rigid.velocity.y);
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
        rigid.AddForce(vec_jump, ForceMode2D.Impulse);
    }

    public virtual void TryAttack(bool isPressed) {
        animator.SetBool(para_attackPressed, isPressed);

        if (intervalCounter < 0 && isPressed) {
            Attack();
        }
    }

    public virtual void Attack() {
        Move(0);
        intervalCounter = status.attackInterval;
        durationCounter = status.attackDuration;
        animator.SetTrigger(para_attack);
        curr_state = CharacterState.attack;
    }

    public virtual void Skill() {
        animator.SetTrigger(para_skill);
        curr_state = CharacterState.skill;
    }

    protected abstract void Shoot();

    public virtual void Hit(BulletData bulletData) {
        curHP -= bulletData.dmg;
        if (curHP <= 0) {
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

    public virtual void GetEvent(string eventName) {
        switch (eventName) {
            case "fire":
                Shoot();
                break;
        }
    }

    public void FlipModel(bool flip) {
        mecanim.skeleton.ScaleX = flip ? -1 : 1;
    }

    private bool IsGrounded() {
        return groundChecker.isGrounded;
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, status.range);
    }
}
