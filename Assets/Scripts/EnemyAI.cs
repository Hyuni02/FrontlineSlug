using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState {
        None,
        Chase,
        Attack,
        Die
    }

    //�÷��̾� ã��
    public GameObject player;
    public int range;

    private EnemyState prev_state = EnemyState.None;
    public EnemyState curr_state = EnemyState.None;

    public Enemy enemy;

    public AIState curr_aiState;
    public AIState aiState_Chase;
    public AIState aiState_Attack;
    public AIState aiState_Die;

    private void Start() {
        enemy = GetComponent<Enemy>();

        aiState_Attack = new AIState_Attack(this);
        aiState_Chase = new AIState_Chase(this);
        aiState_Die = new AIState_Die(this);
    }

    private void Update() {
        if (curr_state == EnemyState.Die) return;
        if (!enemy.activate) return;

        SelectState();

        if (prev_state != curr_state) {
            prev_state = curr_state;
            StateChanged();
        }

        curr_aiState.Update();
    }

    //�� ���� : ����, ����
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
