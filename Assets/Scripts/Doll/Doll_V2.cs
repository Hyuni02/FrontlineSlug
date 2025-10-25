using UnityEngine;
using Spine;
using Spine.Unity;
using System.Collections.Generic;
using System.Reflection;

public class Doll_V2 : MonoBehaviour
{
    public enum CharacterState {
        none, wait, move, attack, die
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

    protected virtual void Start() {
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
        
        if (moving) FlipModel(hori < 0);
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
    
    private void FlipModel(bool flip) {
        mecanim.skeleton.ScaleX = flip ? -1 : 1;
    }
    
    private bool IsGrounded() {
        return groundChecker.isGrounded;
    }
}
