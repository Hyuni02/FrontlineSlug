using System;
using System.Collections.Generic;
using System.Linq;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = UnityEngine.Event;

public abstract class Doll : MonoBehaviour {
    public enum CharacterState {
        none, wait, move, attack, die
    }

    protected Vector2 vec_move;
    protected Vector2 vec_jump;

    protected Rigidbody2D rigid;
    protected SkeletonAnimation skel;
    protected Animator animator;

    protected string param_hori = "hori";
    protected string param_die = "die";
    protected string param_attack = "attack";
    protected string param_attackCounter = "attackCounter";
    protected string param_attackPressed = "attackPressed";

    public GameObject pref_bullet;
    protected Transform trans_muzzle;

    protected CharacterState prev_state = CharacterState.none;
    [SerializeField]
    protected CharacterState curr_state = CharacterState.none;

    public Spine.Animation TargetAnimation { get; private set; }
    public List<StateAnimationPair> lst_stateAnimation = new List<StateAnimationPair>();

    [Header("Events")]
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string evt_fire;
    Spine.EventData eventData_fire;

    protected int maxHP = 100;
    [SerializeField]
    protected int currHP = 100;
    protected float speed = 7;
    protected int jumpPower = 800;
    protected float attackInterval = 1.5f;
    protected float intervalCounter = 0;
    protected float attakDuration = 0.5f;
    protected float durationCounter = 0;

    protected virtual void Start() {
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
        trans_muzzle = GetComponentInChildren<BoneFollower>().transform;

        eventData_fire = skel.Skeleton.Data.FindEvent(evt_fire);
        skel.AnimationState.Event += HandleAnimationStateEvent;
    }

    protected virtual void Update() {
        intervalCounter -= Time.deltaTime;
        durationCounter -= Time.deltaTime;
        animator.SetFloat(param_attackCounter, durationCounter);

        if (prev_state != curr_state) {
            HandleStateChanged();
            prev_state = curr_state;
        }
    }

    public virtual void Move(float hori) {
        if (curr_state == CharacterState.die) return;
        if (durationCounter > 0) return;

        vec_move = new Vector2(hori * speed, rigid.velocity.y);
        rigid.velocity = vec_move;
        if (hori != 0) skel.skeleton.ScaleX = hori > 0 ? 1 : -1;
        animator.SetFloat(param_hori, Mathf.Abs(hori));
        curr_state = hori != 0 ? CharacterState.move : CharacterState.wait;
    }

    public virtual void Jump() {
        if (durationCounter > 0) return;

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(vec_jump);
    }

    public virtual void TryAttack(bool isPressed) {
        animator.SetBool(param_attackPressed, isPressed);

        if (intervalCounter < 0 && isPressed) {
            Attack();
        }
    }

    public virtual void Attack() {
        //점프 중 공격 불가 

        rigid.velocity = Vector2.zero;
        intervalCounter = attackInterval;
        durationCounter = attakDuration;
        animator.SetTrigger(param_attack);
        curr_state = CharacterState.attack;
    }

    public virtual void Hit(BulletData bulletData) {
        currHP -= bulletData.dmg;
        if (currHP <= 0) {
            curr_state = CharacterState.die;
            print($"{gameObject.name} died.");
        }
    }
    public virtual void Shoot() {
        GameObject obj = Instantiate(pref_bullet, trans_muzzle.position, Quaternion.identity);
        obj.GetComponent<Bullet>().init(new BulletData(gameObject, 51, 24, Vector2.right));
    }

    #region Animation
    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e) {
        bool fire = (eventData_fire == e.Data); // Performance recommendation: Match cached reference instead of string.
        if (fire) {
            Shoot();
        }
    }

    void HandleStateChanged() {
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

    private void PlayAnimationForState(string stateShortName, int layerIndex) {
        PlayAnimationForState(StringToHash(stateShortName), layerIndex,
            !(stateShortName.Contains("die") || stateShortName.Contains("attack")));
    }

    private void PlayAnimationForState(int shortNameHash, int layerIndex, bool loop = true) {
        var foundAnimation = GetAnimationForState(shortNameHash);
        if (foundAnimation == null)
            return;

        PlayNewAnimation(foundAnimation, layerIndex, loop);
    }

    private Spine.Animation GetAnimationForState(int shortNameHash) {
        var foundState = lst_stateAnimation.Find(entry => StringToHash(entry.stateName) == shortNameHash);
        return (foundState == null) ? null : foundState.animation;
    }

    private void PlayNewAnimation(Spine.Animation target, int layerIndex, bool loop = true) {
        Spine.Animation transition = null;
        Spine.Animation current = null;

        current = GetCurrentAnimation(layerIndex);

        skel.AnimationState.SetAnimation(layerIndex, target, loop);

        this.TargetAnimation = target;
    }

    private Spine.Animation GetCurrentAnimation(int layerIndex) {
        var currentTrackEntry = skel.AnimationState.GetCurrent(layerIndex);
        return (currentTrackEntry != null) ? currentTrackEntry.Animation : null;
    }

    private int StringToHash(string s) {
        return Animator.StringToHash(s);
    }
    #endregion
}
