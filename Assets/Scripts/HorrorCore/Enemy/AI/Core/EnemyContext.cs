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
    [Header("--- REFERENCES ---")]
    [Tooltip("Reference to the player's Transform (not serialized).")]
    [NonSerialized] public Transform playerTransform;

    [Tooltip("The NavMeshAgent component controlling enemy movement.")]
    public NavMeshAgent navAgent;

    [Tooltip("Animator component for controlling enemy animations.")]
    public Animator animator;

    // --- Vision Sensor ---
    [Header("--- VISION SENSOR SETTINGS ---")]
    [Tooltip("Custom sensor component for detecting the player in a cone of vision.")]
    public VisionSensor visionSensor;

    [Tooltip("Flag indicating if the player is currently visible.")]
    public bool playerInVision;

    [Tooltip("Offset for aligning the player's head position for targeting.")]
    public Vector3 playerHeadOffset;

    [Tooltip("Offset for aligning the enemy's head position for vision calculations.")]
    public Vector3 enemyHeadOffset;

    [Tooltip("Time elapsed since the enemy last saw the player.")]
    public float timeSinceLastPlayerSight;

    // --- Audio Detection Sensor ---
    [Header("--- AUDIO DETECTION SENSOR SETTINGS ---")]
    [Tooltip("Position of the last noise heard by the enemy.")]
    public Vector3 lastHeardNoisePosition;

    [Tooltip("Flag indicating if there was a recent noise event.")]
    public bool hasRecentNoise;

    [Header("~ Loudness threshold to trigger an alert state")]
    public float alertThreshold;

    [Header("~ Loudness threshold to trigger an investigation state (n/a)")]
    public float investigateThreshold;

    [Tooltip("Accumulated loudness value from noise inputs.")]
    public float accumulateLoudness;

    [Header("~ Distance at which enemy HEARS the player and starts pursuing")]
    public float immediatePursuitDistance;

    // --- State Exit Timers ---
    [Header("--- STATE EXIT TIMERS ---")]
    [Header("~ Duration to investigate before returning back to roaming")]
    public float investigateTimeout;

    [Header("~ Time allowed without sighting the player before giving up pursuit")]
    public float pursuingVisionLostTime;

    [Header("~ Duration of the GettingAlert state")]
    public float alertDuration = 1.5f;

    public float deathDuration = 3f;

    // --- Movement / Patrol Data ---
    [Header("--- MOVEMENT / PATROL DATA ---")]
    [Header("~ Enemy speed during roaming state")]
    public float roamSpeed;

    [Header("~ Enemy speed during pursuit state")]
    public float chaseSpeed;

    [Header("~ Array of patrol waypoints for roaming behavior")]
    public Transform[] patrolPoints;

    [Tooltip("Index of the current patrol waypoint being targeted.")]
    public int currentPatrolIndex;

    // --- Additional Design Tweaks ---
    [Header("--- KILL STATE SETTINGS ---")]
    [Header("Distance threshold within which the enemy can kill the player.")]
    public float killDistance;

    // Initializes the player's transform reference.
    public void InitPlayer(Transform pPlayerTransform)
    {
        playerTransform = pPlayerTransform;
        // Uncomment the following line for debug confirmation of player initialization.
        // if(playerTransform) Debug.Log("PLAYER INITIALIZED!!!");
    }
    public void ChangePlayerHeadOffset()
    { 
        playerHeadOffset = playerTransform.gameObject.GetComponentInChildren<Camera>().gameObject.transform.localPosition;
    }
}
