using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyContext
{
    // References
    public NavMeshAgent navAgent;
    public Animator animator;
    public AudioSensor audioSensor;
    public VisionSensor visionSensor;
    public Transform playerTransform;

    // AI-Director data
    public Vector3 lastHeardNoisePosition;
    public bool hasRecentNoise;
    
    // Suspicion / detection parameters
    public float alertThreshold;        // e.g., how many decibels or “loudness units” needed
    public float investigateTimeout;    // Time in Investigating before returning to Roaming
    public float pursuingVisionLostTime; // Time we can’t see the player before giving up

    // Timers / counters / states
    public float timeSinceLastPlayerSight;
    public float accumulateLoudness;
    public bool playerInVision;
    
    // Movement/Patrol data
    public float roamSpeed;
    public float chaseSpeed;
    public Transform[] patrolPoints; // if you want a set route
    public int currentPatrolIndex;
    
    // Additional design tweak fields...
    public float killDistance;
}