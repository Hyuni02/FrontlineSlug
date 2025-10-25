using UnityEngine;

public class EnemyAI_V2 : MonoBehaviour
{
    public enum EnemyState {
        None,
        Wait,
        Move,
        Attack,
        Die
    }
    
    //플레이어 찾기
    [HideInInspector]
    public GameObject player;
    public int range;

    //State
    private EnemyState prev_state = EnemyState.None;
    public EnemyState curr_state = EnemyState.Wait;

    [HideInInspector]
    public Enemy_V2 enemy;

    public AIState_V2 curr_aiState;
    public AIState_V2 aiState_Wait;
    public AIState_V2 aiState_Move;
    public AIState_V2 aiState_Attack;
    public AIState_V2 aiState_Die;

    public bool activate = false;
    
    private void Start() {
        enemy = GetComponent<Enemy_V2>();

        SetState();

        player = PlayerController_V2.instance.player.gameObject;
        
        StateChanged();
    }

    protected virtual void SetState() {
        aiState_Wait = new AIState_Wait_V2(this);
        aiState_Attack = new AIState_Attack_V2(this);
        aiState_Move = new AIState_Move_V2(this);
        aiState_Die = new AIState_Die_V2(this);
    }
    
    private void Update() {
        StateChanged();

        curr_aiState.Update();
    }
    
    public virtual void Activate() {
        activate = true;
        ChangeState(EnemyState.Move);
    }
    
    public void ChangeState(EnemyState state) {
        curr_state = state;
        StateChanged();
    }

    private void StateChanged() {
        if (prev_state == curr_state) return;
        prev_state = curr_state;
        curr_aiState?.Exit();
        if(curr_state == EnemyState.Attack) {
            curr_aiState = aiState_Attack;
        }
        else if(curr_state == EnemyState.Move) {
            curr_aiState = aiState_Move;
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
