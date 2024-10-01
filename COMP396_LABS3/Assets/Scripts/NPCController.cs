using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCState_AT
    {
        Patrol,
        Attack,
        RunAway,
        //new states
        Idle,
        Heal,

    }

    public NPCState_AT currentState;
    public GameObject other;

    [Header("Temporary (Mock) Variables")]
    [SerializeField] bool bSafe;
    [SerializeField] int HealthPoints=100;
    [SerializeField] int EnemyHealthPoints = 50;

    void Start()
    {
        currentState=NPCState_AT.Patrol;
    }

    void Update()
    {
        SimpleFSMUpdate();
    }

    private void SimpleFSMUpdate()
    {
        switch (currentState)
        {
            case NPCState_AT.Idle:
                HandleIdle();
                break;

            case NPCState_AT.Heal:
                HandleHeal();
                break;

            case NPCState_AT.Patrol:
                HandlePatrol();
                // if (Threatened())
                // {
                //     if (StrongerThanEnemy())
                //     {
                //         ChangeState(NPCState_AT.Attack);
                //     }
                //     else
                //     {
                //         ChangeState(NPCState_AT.Runaway);
                //     }
                // }
                break;

            case NPCState_AT.Attack:
                HandleAttack();
                // if (!StrongerThanEnemy())
                // {
                //     ChangeState(NPCState_AT.Runaway);
                // }
                break;

            case NPCState_AT.RunAway:
                HandleRunaway();
                break;

            default:
                Debug.LogError($"**** Error: currentState={currentState} is invalid.");
                break;
        }
    }

    private void ChangeState(NPCState_AT newState)
    {
        currentState = newState;
    }

    private void HandleIdle()
    {
        if(AroundNpc())
        {
            ChangeState(NPCState_AT.Patrol);
        }
        else
        {
            ChangeState(NPCState_AT.Idle);
        }
    }

    private void HandleHeal()
    {
        if(Unhealthy())
        {
            ChangeState(NPCState_AT.Heal);
        }
        else
        {
            ChangeState(NPCState_AT.Patrol);
        }
    }

    private void HandlePatrol()
    {

        if (Threatened())
        {
            if (StrongerThanEnemy())
            {
                ChangeState(NPCState_AT.Attack);
            }
            else
            {
                ChangeState(NPCState_AT.RunAway);
            }
        }
                FollowPatrolPath();
    }

    private void HandleAttack()
    {
        if (WeakerThanEnemy())
        {
            ChangeState(NPCState_AT.RunAway);
        }
        else
        {
            Attack();
        }
    }

    private void HandleRunaway()
    {
        EvadeEnemy();
        if (Safe())
        {
            ChangeState(NPCState_AT.Patrol);
        }
    }

    private void EvadeEnemy()
    {
        print($"EvadeEnemy");

        //if (Safe())
        //{
        //    ChangeState(NPCState_AT.Patrol);
        //}
    }

    private void FollowPatrolPath()
    {
        print($"In FollowPatrolPath"); //MOCK
    }

    private bool Safe()
    {
        return bSafe;
    }

    private bool Threatened()
    {
        return !Safe(); 
    }

    private bool StrongerThanEnemy()
    {
        return HealthPoints >= EnemyHealthPoints; //MOCK
    }

    private bool WeakerThanEnemy()
    {
        return !StrongerThanEnemy();
    }

    private bool Unhealthy()
    {
        return true;
    }

    private bool AroundNpc()
    {
        return true;
    }

    
    private void Attack()
    {
        print($"Attack-ing!!!");
    }
}



