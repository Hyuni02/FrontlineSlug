using System;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum CharacterState {
        none, wait, move, attack, die
    }
    
    private Rigidbody2D rigid;
    private SkeletonAnimation skel;
    private Animator animator;

    private float speed = 7;
    private float jumpPower = 800;
    private float attackInterval = 1.5f;
    private float intervalCounter = 0;
    private float attakDuration = 0.5f;
    private float durationCounter = 0;

    private Vector2 vec_move;
    private Vector2 vec_jump;

    private CharacterState prev_state = CharacterState.none;
    [SerializeField]
    private CharacterState curr_state = CharacterState.none;
    
    private string param_hori = "hori";
    private string param_die = "die";
    private string param_attack = "attack";
    private string param_attackCounter = "attackCounter";

    public Spine.Animation TargetAnimation { get; private set; }
    public List<StateAnimationPair> lst_stateAnimation = new List<StateAnimationPair>();

    void Start() {
        //set Component
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skel = GetComponentInChildren<SkeletonAnimation>();

        //init animation
        foreach (var entry in lst_stateAnimation) {
            entry.animation.Initialize();
        }
        
        //init variable
        vec_jump = new Vector2(0, jumpPower);
    }

    private void Update() {
        intervalCounter -= Time.deltaTime;
        durationCounter -= Time.deltaTime;
        animator.SetFloat(param_attackCounter, durationCounter);
        
        if (prev_state != curr_state) {
            HandleStateChanged();
            prev_state = curr_state;
        }
    }

    public void Move(float hori) {
        if (durationCounter > 0) return;
        
        vec_move = new Vector2(hori * speed, rigid.velocity.y);
        rigid.velocity = vec_move;
        if (hori != 0) skel.skeleton.ScaleX = hori > 0 ? 1 : -1;
        animator.SetFloat(param_hori, Mathf.Abs(hori));
        curr_state = hori != 0 ? CharacterState.move : CharacterState.wait;
    }

    public void Jump() {
        if (durationCounter > 0) return;
        
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(vec_jump);
    }

    public void Attack() {
        //점프 중 공격 불가 
        
        if (intervalCounter < 0) {
            rigid.velocity = Vector2.zero;
            intervalCounter = attackInterval;
            durationCounter = attakDuration;
            animator.SetTrigger(param_attack);
            curr_state = CharacterState.attack;
        }
    }
    
    void HandleStateChanged () {
        string stateName = null;
        switch (curr_state) {
            case CharacterState.wait:
                stateName = "wait";
                break;
            case CharacterState.move:
                stateName = "move";
                break;
            case CharacterState.attack:
                stateName = "attack";
                break;
            case CharacterState.die:
                stateName = "die";
                break;
            default:
                break;
        }

        PlayAnimationForState(stateName, 0);
    }
    
    private void PlayAnimationForState (string stateShortName, int layerIndex) {
        PlayAnimationForState(StringToHash(stateShortName), layerIndex);
    }
    
    private void PlayAnimationForState (int shortNameHash, int layerIndex) {
        var foundAnimation = GetAnimationForState(shortNameHash);
        if (foundAnimation == null)
            return;

        PlayNewAnimation(foundAnimation, layerIndex);
    }
    
    private Spine.Animation GetAnimationForState (int shortNameHash) {
        var foundState = lst_stateAnimation.Find(entry => StringToHash(entry.stateName) == shortNameHash);
        return (foundState == null) ? null : foundState.animation;
    }
    
    private void PlayNewAnimation (Spine.Animation target, int layerIndex) {
        Spine.Animation transition = null;
        Spine.Animation current = null;

        current = GetCurrentAnimation(layerIndex);
        
        skel.AnimationState.SetAnimation(layerIndex, target, true);
        
        this.TargetAnimation = target;
    }
    
    private Spine.Animation GetCurrentAnimation (int layerIndex) {
        var currentTrackEntry = skel.AnimationState.GetCurrent(layerIndex);
        return (currentTrackEntry != null) ? currentTrackEntry.Animation : null;
    }
    
    private int StringToHash (string s) {
        return Animator.StringToHash(s);
    }
}
