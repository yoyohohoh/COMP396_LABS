using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController2 : MonoBehaviour
{
    StateMachine stateMachine;
    StateMachine.State patrolState;
    StateMachine.State attackState;
    StateMachine.State runAwayState;

    public GameObject goOpponent;

    [Header("Temporary (Mock Variables)")]
    [SerializeField] bool bSafe;
    [SerializeField] int HealthPoints = 100;
    [SerializeField] int EnemyHealthPoints = 50;
    [SerializeField] float distanceCutOff = 5;
    private float sqrDistanceCutoff;
    public float speed = 5f; // m/s

    public float originalY;

    public Transform[] Waypoints;
    public int currentWaypointIndex;
    public Transform currentWaypoint; // for convenience
    public Transform nextWaypoint;

    public float TOL = 0.001f; // when do we consider NPC is IN the next WP.
    Func<int, int, int> nextWaypointIndex = (i, NwP) => (i + 1) % NwP; //
    public int NumberOfWaypoints;

    // rotation speed
    float rotSpeed = 60f; // 60 degrees per sec

    // Start is called before the first frame update
    void Start()
    {
        NumberOfWaypoints = Waypoints.Length; // Initialize number of waypoints first
        originalY = this.transform.position.y;
        currentWaypointIndex = 0;
        currentWaypoint = Waypoints[currentWaypointIndex];
        nextWaypoint = Waypoints[nextWaypointIndex(currentWaypointIndex, NumberOfWaypoints)];

        sqrDistanceCutoff = distanceCutOff * distanceCutOff;
        stateMachine = new StateMachine();

        // Create states
        patrolState = stateMachine.CreateState("Patrol");
        attackState = stateMachine.CreateState("Attack");
        runAwayState = stateMachine.CreateState("RunAway");

        // Set up Patrol state
        patrolState.OnEnter = () =>
        {
            Debug.Log("Entering Patrol State");
            // Add any setup logic for entering Patrol state
        };
        patrolState.OnExit = () =>
        {
            Debug.Log("Exiting Patrol State");
            // Add any cleanup logic for exiting Patrol state
        };
        patrolState.OnFrame = () =>
        {
            Debug.Log("Patrol State Update");
            // Add logic to execute every frame while in Patrol state
            PatrolOnFrame(); // Call your method to handle Patrol logic
        };

        // Set up Attack state
        attackState.OnEnter = () =>
        {
            Debug.Log("Entering Attack State");
            // Add any setup logic for entering Attack state
        };
        attackState.OnExit = () =>
        {
            Debug.Log("Exiting Attack State");
            // Add any cleanup logic for exiting Attack state
        };
        attackState.OnFrame = () =>
        {
            Debug.Log("Attack State Update");
            // Add logic to execute every frame while in Attack state
            AttackOnFrame(); // Call your method to handle Attack logic
        };

        // Set up RunAway state
        runAwayState.OnEnter = () =>
        {
            Debug.Log("Entering RunAway State");
            // Add any setup logic for entering RunAway state
        };
        runAwayState.OnExit = () =>
        {
            Debug.Log("Exiting RunAway State");
            // Add any cleanup logic for exiting RunAway state
        };
        runAwayState.OnFrame = () =>
        {
            Debug.Log("RunAway State Update");
            // Add logic to execute every frame while in RunAway state
            RunAwayOnFrame(); // Call your method to handle RunAway logic
        };

        // Set initial state and mark state machine as ready
        stateMachine.ready = true;
    }

    void Update()
    {
        // Update bSafe based on NPC's proximity to goOpponent
        bSafe = Safe();
        stateMachine.Update();
    }

    void PatrolOnFrame()
    {
        Debug.Log("Patrol.OnFrame...");
        if (Threatened())
        {
            if (StrongerThanEnemy())
            {
                // Transition to Attack if stronger
                stateMachine.TransitionTo(attackState);
            }
            else
            {
                // Transition to RunAway if weaker
                stateMachine.TransitionTo(runAwayState);
            }
        }
        FollowPatrolPath();
    }

    void AttackOnFrame()
    {
        Debug.Log("Attack.OnFrame...");

        // Check if the NPC should stop attacking based on the opponent's distance
        if (Safe())
        {
            // Transition back to Patrol if the opponent is far away
            stateMachine.TransitionTo(patrolState);
        }
        else if (!StrongerThanEnemy())
        {
            // Transition to RunAway if weaker
            stateMachine.TransitionTo(runAwayState);
        }
        else
        {
            // Continue attacking
            AttackEnemy();
        }
    }

    void RunAwayOnFrame()
    {
        Debug.Log("RunAway.OnFrame...");
        if (Safe())
        {
            // Transition back to Patrol if safe
            stateMachine.TransitionTo(patrolState);
        }
        else
        {
            // Continue evading
            EvadeEnemy();
        }
    }

    // Check if NPC is within a safe distance from goOpponent
    private bool Safe()
    {
        Vector3 headingNPC2Opponent = goOpponent.transform.position - this.transform.position;
        float sqrDistance = headingNPC2Opponent.sqrMagnitude;
        // Return true if the NPC is farther than the cutoff distance, meaning it is safe
        return sqrDistance > sqrDistanceCutoff;
    }

    private void EvadeEnemy()
    {
        Debug.Log("EvadeEnemy...");
        Vector3 headingNPC2Opponent = goOpponent.transform.position - this.transform.position;
        headingNPC2Opponent.Normalize(); // make it a unit vector
        transform.Translate(-headingNPC2Opponent * speed * Time.deltaTime);
    }

    private void AttackEnemy()
    {
        Debug.Log("Attacking the enemy...");
        // Implement attack logic here, e.g., reducing enemy health
        EnemyHealthPoints -= 1; // Mock logic for attacking and reducing enemy health
    }

    private void FollowPatrolPath()
    {
        Vector3 heading2NextWP = nextWaypoint.position - this.transform.position; // Fixed logic to move towards next WP from NPC's position

        if (Vector3.SqrMagnitude(heading2NextWP) < TOL * TOL) // Compare TOL squared for consistency
        {
            currentWaypointIndex = nextWaypointIndex(currentWaypointIndex, NumberOfWaypoints);
            currentWaypoint = Waypoints[currentWaypointIndex];
            nextWaypoint = Waypoints[nextWaypointIndex(currentWaypointIndex, NumberOfWaypoints)];
        }

        // Rotate towards the next waypoint
        heading2NextWP.y = 0f; // Keep NPC level
        Quaternion targetRotation = Quaternion.LookRotation(heading2NextWP);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * rotSpeed);

        // Move towards the next waypoint
        this.transform.Translate(heading2NextWP.normalized * Time.deltaTime * speed);
    }

    private bool StrongerThanEnemy()
    {
        return HealthPoints > EnemyHealthPoints;
    }

    private bool Threatened()
    {
        return !bSafe;
    }
}
