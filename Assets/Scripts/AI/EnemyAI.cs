using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState {
        None,
        Wait,
        Patrol,
        Chase,
        Attack,
        Die
    }

    //플레이어 찾기
    [HideInInspector]
    public GameObject player;
    public int range;
    public bool patrol;

    private EnemyState prev_state = EnemyState.None;
    public EnemyState curr_state = EnemyState.Wait;

    [HideInInspector]
    public Enemy enemy;

    public AIState curr_aiState;
    public AIState aiState_Chase;
    public AIState aiState_Patrol;
    public AIState aiState_Attack;
    public AIState aiState_Wait;
    public AIState aiState_Die;

    public bool activate = false;
    
    private void Start() {
        enemy = GetComponent<Enemy>();

        aiState_Attack = new AIState_Attack(this);
        aiState_Chase = new AIState_Chase(this);
        aiState_Patrol = new AIState_Patrol(this);
        aiState_Wait = new AIState_Wait(this);
        aiState_Die = new AIState_Die(this);

        player = PlayerContoller.instance.player.gameObject;
        
        StateChanged();
    }

    private void Update() {
        if (curr_state == EnemyState.Die) return;

        SelectState();

        if (prev_state != curr_state) {
            prev_state = curr_state;
            StateChanged();
        }

        curr_aiState.Update();
    }

    public virtual void Activate() {
        activate = true;
    }

    //적 상태 : 추적, 공격
    protected virtual void SelectState() {
        if (enemy.currHP <= 0) {
            curr_state = EnemyState.Die;
        }
        
        if (!activate) {
            curr_state = EnemyState.Wait;
            return;
        }
        
        var distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > range) {
            if (patrol)
                curr_state = EnemyState.Patrol;
            else
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
        else if(curr_state == EnemyState.Patrol) {
            curr_aiState = aiState_Patrol;
        }
        else if (curr_state == EnemyState.Wait) {
            curr_aiState = aiState_Wait;
        }
        else if (curr_state == EnemyState.Die) {
            curr_aiState = aiState_Die;
        }
        else if (curr_state == EnemyState.None) {
            return;
        }
        curr_aiState.Enter();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
