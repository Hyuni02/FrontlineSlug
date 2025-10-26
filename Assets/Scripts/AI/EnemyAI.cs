using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState {
        None,
        Wait,
        Move,
        Attack,
        Skill,
        Die
    }
    
    public int range;

    //State
    private EnemyState prev_state = EnemyState.None;
    public EnemyState curr_state = EnemyState.Wait;

    [HideInInspector]
    public Enemy enemy;

    public AIState curr_aiState;
    public AIState aiState_Wait;
    public AIState aiState_Move;
    public AIState aiState_Attack;
    public AIState aiState_Skill;
    public AIState aiState_Die;

    public bool activate = false;
    
    protected virtual void Start() {
        enemy = GetComponent<Enemy>();

        SetState();
        
        StateChanged();
    }

    protected virtual void SetState() {
        aiState_Wait = new AIState_Wait(this);
        aiState_Attack = new AIState_Attack(this);
        aiState_Move = new AIState_Move(this);
        aiState_Die = new AIState_Die(this);
    }
    
    private void Update() {
        if (!activate) return;
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
        else if (curr_state == EnemyState.Skill) {
            curr_aiState = aiState_Skill;
        }
        else if (curr_state == EnemyState.None) {
            return;
        }
        curr_aiState.Enter();
    }

    public void LookTarget(Transform target) {
        enemy.FlipModel((target.transform.position - transform.position).x < 0);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
