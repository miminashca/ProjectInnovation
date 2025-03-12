using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Holds shared data for the Enemy AI (NavMeshAgent, sensors, player references, etc.).
/// This is serialized in the EnemyStateMachine so you can configure values in the Inspector.
/// </summary>
[System.Serializable]
public class EnemyContext
{
    // --- References ---
    [NonSerialized] public Transform playerTransform;
    public NavMeshAgent navAgent;      // Reference to the NavMeshAgent component on the Enemy.
    public Animator animator;          // Animator controlling the Enemy’s animations.
    //public AudioSensor audioSensor;    // Custom sensor that detects loudness.
    public VisionSensor visionSensor;  // Custom sensor that detects player in a cone of vision.
    public Vector3 playerHeadOffset;
    public Vector3 enemyHeadOffset;

    // --- AI Director data ---
    public Vector3 lastHeardNoisePosition;
    public bool hasRecentNoise;

    // --- Suspicion / detection parameters ---
    public float alertThreshold;         // Loudness threshold to trigger GettingAlert state.
    public float investigateTimeout;     // Time in Investigating before returning to Roaming.
    public float pursuingVisionLostTime; // Time we can’t see the player before giving up.

    // --- Timers / counters / states ---
    public float timeSinceLastPlayerSight;
    public float accumulateLoudness;
    public bool playerInVision;

    // --- Movement / Patrol data ---
    public float roamSpeed;             // Movement speed during Roaming.
    public float chaseSpeed;            // Movement speed during Pursuing.
    public Transform[] patrolPoints;    // Array of transforms for potential waypoints.
    public int currentPatrolIndex;      // Tracks which waypoint we’re currently heading to.

    // --- Additional design tweak fields ---
    public float killDistance;          // Distance at which the Enemy can kill the player.
    
    public void InitPlayer(Transform pPlayerTransform)
    { 
        playerTransform = pPlayerTransform;
        //if(playerTransform) Debug.Log("PLAYER INITIALIZED!!!");
    }
}
