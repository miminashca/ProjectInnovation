using UnityEngine;

public class KillingState : IEnemyState
{
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }
    public KillingState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.Killing;
    }
    public void Enter(EnemyContext context)
    {
        Debug.Log("Enter killing state!");
        context.animator.SetBool("IsKilling", true);

        // 1) Stop enemy movement
        context.navAgent.isStopped = true;
        context.navAgent.ResetPath();

        // Optionally disable the player’s movement
        var playerMovement = context.playerTransform.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // 2) Enforce a gap between enemy and player
        float desiredDistance = 1.5f; // or whatever feels right
        Vector3 toPlayer = context.playerTransform.position - context.navAgent.transform.position;
        toPlayer.y = 0f; // only consider horizontal distance

        float currentDistance = toPlayer.magnitude;
        if (currentDistance < desiredDistance)
        {
            // Move the enemy backward so that the distance to the player is at least desiredDistance
            float difference = desiredDistance - currentDistance;
            context.navAgent.transform.position -= toPlayer.normalized * difference;
        }

        // 3) Rotate the enemy to face the player
        Vector3 directionToPlayer = context.playerTransform.position - context.navAgent.transform.position;
        directionToPlayer.y = 0f;
        context.navAgent.transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // 4) Rotate the player to face the enemy
        Vector3 directionToEnemy = context.navAgent.transform.position - context.playerTransform.position;
        directionToEnemy.y = 0f;
        context.playerTransform.rotation = Quaternion.LookRotation(directionToEnemy);

        // 5) Notify the player side to begin the "death" sequence (camera fall, etc.)
        AIDirector.KillPlayer();
    }


    public void Execute(EnemyContext context)
    {
        
    }

    public void Exit(EnemyContext context)
    {

    }
    
    public void OnKillAnimationEnd()
    {
        Debug.Log("Kill animation ended, triggering LoseGame event.");
        EventBus.LoseGame();
    }
}