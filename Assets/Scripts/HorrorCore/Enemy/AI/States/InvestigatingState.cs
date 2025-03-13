using UnityEngine;

public class InvestigatingState : IEnemyState
{
    private float investigateTimer = 0f;
    private Vector3[] investigationPoints;
    private int currentInvestigationIndex = 0;

    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }

    public InvestigatingState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.Investigating;
    }

    public void Enter(EnemyContext context)
    {
        Debug.Log("Enter Investigating state");
        // Reset accumulated noise when starting investigation.
        context.accumulateLoudness = 0f;
        investigateTimer = 0f;
        // Set investigation speed.
        context.navAgent.speed = context.chaseSpeed;
        context.animator.SetBool("IsInvestigating", true);

        // Generate investigation points around the last heard noise position.
        investigationPoints = GenerateInvestigationPoints(context.lastHeardNoisePosition);
        currentInvestigationIndex = 0;

        // Move to the first investigation point.
        context.navAgent.SetDestination(investigationPoints[currentInvestigationIndex]);
    }

    public void Execute(EnemyContext context)
    {
        investigateTimer += Time.deltaTime;

        // If the player is spotted, switch to pursuing.
        if (context.playerInVision)
        {
            SM.SetState(new PursuingState(SM));
            return;
        }

        // If the current point is reached...
        if (!context.navAgent.pathPending && context.navAgent.remainingDistance < 1f)
        {
            if (currentInvestigationIndex < investigationPoints.Length - 1)
            {
                // Move to the next investigation point.
                currentInvestigationIndex++;
                context.navAgent.SetDestination(investigationPoints[currentInvestigationIndex]);
            }
            // If all points are visited and time's up, revert to roaming.
            else if (investigateTimer >= context.investigateTimeout)
            {
                SM.SetState(new RoamingState(SM));
                return;
            }
        }
    }

    public void Exit(EnemyContext context)
    {
        context.animator.SetBool("IsInvestigating", false);
    }

    // Generates investigation points around a center point.
    private Vector3[] GenerateInvestigationPoints(Vector3 center)
    {
        Vector3[] points = new Vector3[3];
        for (int i = 0; i < points.Length; i++)
        {
            // Random points within a 3-unit radius.
            points[i] = center + new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
        }
        return points;
    }

    // This method draws the investigation points using Gizmos.
    public void DrawInvestigationGizmos()
    {
        if (investigationPoints == null) return;
        Gizmos.color = Color.yellow;
        foreach (Vector3 point in investigationPoints)
        {
            // Draw a small sphere at each investigation point.
            Gizmos.DrawSphere(point, 0.3f);
        }
    }
}
