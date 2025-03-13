using UnityEngine;

/// <summary>
/// The state machine that controls which state the Enemy is in.
/// </summary>
public class EnemyStateMachine : MonoBehaviour
{
    /// <summary>
    /// The current active state implementing IEnemyState.
    /// </summary>
    private IEnemyState currentState;
    public EnemyContext context;

    private void OnEnable()
    {
        EventBus.OnThiefSpawned += context.InitPlayer;
        EventBus.OnPlayerCrouch += context.ChangePlayerHeadOffset;
    }
    private void OnDisable()
    {
        EventBus.OnThiefSpawned -= context.InitPlayer;
        EventBus.OnPlayerCrouch -= context.ChangePlayerHeadOffset;
    }
    private void Start()
    {
        // Initialize with the RoamingState as the default state.
        SetState(new RoamingState(this));
    }

    private void Update()
    {
        // Execute the current state's logic each frame.
        currentState?.Execute(context);
    }

    /// <summary>
    /// Sets a new state, calling Exit() on the old state and Enter() on the new state.
    /// </summary>
    public void SetState(IEnemyState newState)
    {
        // 1) Exit the current state.
        if (currentState != null)
        {
            AIDirector.ExitStateWithID(currentState.enemyStateType);
            currentState.Exit(context);
        }

        // 2) Switch to the new state.
        currentState = newState;

        // 3) Enter the new state.
        if (currentState != null)
        {
            AIDirector.EnterStateWithID(currentState.enemyStateType);
            currentState.Enter(context);
        }
    }

    // Centralized Gizmo drawing.
    private void OnDrawGizmos()
    {
        // Draw investigation points if current state is InvestigatingState.
        if (currentState is InvestigatingState investigatingState)
        {
            investigatingState.DrawInvestigationGizmos();
        }

        // Draw patrol points from the context.
        if (context != null && context.patrolPoints != null)
        {
            Gizmos.color = Color.blue;
            foreach (Transform waypoint in context.patrolPoints)
            {
                if (waypoint != null)
                    Gizmos.DrawWireSphere(waypoint.position, 0.3f);
            }
        }
    }
}
