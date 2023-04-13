using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : State
{
    GameObject safePlace;
    float rotationSpeed = 2.0f;
    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.RUNAWAY;
        agent.speed = 5;
        agent.isStopped = false;
        safePlace = GameObject.FindGameObjectWithTag("Safe");
        agent.SetDestination(safePlace.transform.position);
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {

        if (agent.hasPath)
        {
            Vector3 direction = safePlace.transform.position - npc.transform.position;
            direction.y = 0;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            if (agent.remainingDistance < 2)
            {
                nextState = new Idle(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }

}
