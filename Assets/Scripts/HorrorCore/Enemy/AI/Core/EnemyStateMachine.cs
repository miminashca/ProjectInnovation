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
    public EnemyContext context; // Drag & drop or create in Awake()


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
        // 1) Exit the current state
        if (currentState != null)
        {
            EnemyAiEventBus.ExitStateWithID(currentState.enemyStateType);
            currentState.Exit(context);
        }

        // 2) Switch to the new state
        currentState = newState;

        // 3) Enter the new state
        if (currentState != null)
        {
            EnemyAiEventBus.EnterStateWithID(currentState.enemyStateType);
            currentState.Enter(context);
        }
    }

    void OnDrawGizmos()
    {
        if (context == null || context.patrolPoints == null) return;

        Gizmos.color = Color.green;
        foreach (Transform waypoint in context.patrolPoints)
        {
            if (waypoint == null) continue;
            // Draw a small sphere
            Gizmos.DrawWireSphere(waypoint.position, 0.3f);
        }
    }

}
