using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState currentState;
    [SerializeField] private EnemyContext context; // Drag & drop or create in Awake()

    private void Start()
    {
        // Initialize with Roaming State (or whichever default state you want).
        SetState(new RoamingState(this));
    }

    private void Update()
    {
        // Each frame, just run the current state’s logic.
        currentState?.Execute(context);
    }

    public void SetState(IEnemyState newState)
    {
        // 1) Exit the current state
        if (currentState != null)
        {
            EnemyAiEventBus.ExitStateWithEnum(currentState.enemyStateType);
            currentState.Exit(context);
        }

        // 2) Switch to the new state
        currentState = newState;

        // 3) Enter the new state
        if (currentState != null)
        {
            EnemyAiEventBus.EnterStateWithEnum(currentState.enemyStateType);
            currentState.Enter(context);
        }
    }
}