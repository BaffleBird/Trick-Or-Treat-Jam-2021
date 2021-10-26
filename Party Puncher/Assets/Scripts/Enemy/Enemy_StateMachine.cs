using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_StateMachine : NPC_StateMachine
{
	void Awake()
	{
		States.Add(nameof(State_NPC_Idle), new State_Enemy_Idle(nameof(State_NPC_Idle), this));
		States.Add(nameof(State_NPC_Move), new State_NPC_Move(nameof(State_NPC_Move), this));
		States.Add(nameof(State_NPC_Knockdown), new State_NPC_Knockdown(nameof(State_NPC_Knockdown), this));
		States.Add(nameof(State_NPC_GetUp), new State_NPC_GetUp(nameof(State_NPC_GetUp), this));
		States.Add(nameof(State_NPC_Leave), new State_NPC_Leave(nameof(State_NPC_Leave), this));
		States.Add(nameof(State_NPC_GoForCandy), new State_NPC_GoForCandy(nameof(State_NPC_GoForCandy), this));
		States.Add(nameof(State_NPC_GrabCandy), new State_NPC_GrabCandy(nameof(State_NPC_GrabCandy), this));

		States.Add(nameof(State_Enemy_Scare), new State_Enemy_Scare(nameof(State_Enemy_Scare), this));

		_myStatus = new EntityStatus();

		currentState = States[nameof(State_NPC_Idle)];
		_previousState = currentState.StateName;
		currentState.StartState();

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
		_agent.speed = Random.Range(2, 3.5f);
		_agent.SetDestination(transform.position);
	}

	protected override void OnDrawGizmos()
	{
		if (currentState != null)
			currentState.TestUpdate();
	}

	public override void Knockdown(Vector2 direction, float power, bool fear) //Feed it a direction to be pushed, how powerful it is, and whether or not it causes fear
	{
		myInputs.ForceMove(direction * power);
		myInputs.ForceInput(nameof(State_NPC_Knockdown));
		//if (power >= 20) Flee();
	}

	public override void CandyPull(float pullTime, Vector2 targetPosition) //Don't grab the candy.
	{
		return;
	}

	public void Die()
	{
		//Run Dying input
	}
}
