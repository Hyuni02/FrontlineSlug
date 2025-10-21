using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState {
        None,
        Chase,
        Attack
    }

    //플레이어 찾기
    public GameObject player;
    public int range;

    private float initAttackDelay = 1;
    private float attackInterval = 1.5f;
    private float intervalCounter = 0;
    protected float attakDuration = 0.5f;
    protected float durationCounter = 0;

    private EnemyState prev_state = EnemyState.None;
    public EnemyState curr_state = EnemyState.None;

    public Enemy enemy;

    public AIState curr_aiState;
    public AIState aiState_Chase;
    public AIState aiState_Attack;

    private void Start() {
        enemy = GetComponent<Enemy>();

        aiState_Attack = new AIState_Attack(this);
        aiState_Chase = new AIState_Chase(this);
    }

    private void Update() {
        SelectState();

        if (prev_state != curr_state) {
            prev_state = curr_state;
            StateChanged();
        }

        curr_aiState.Update();
    }

    //적 상태 : 추적, 공격
    private void SelectState() {
        var distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > range) {
            curr_state = EnemyState.Chase;
        }
        else {
            curr_state = EnemyState.Attack;
        }
    }

    private void StateChanged() {
        curr_aiState?.Exit();
        if (curr_state == EnemyState.Chase) {
            curr_aiState = aiState_Chase;
        }
        else if(curr_state == EnemyState.Attack) {
            curr_aiState = aiState_Attack;
        }
        curr_aiState.Enter();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
