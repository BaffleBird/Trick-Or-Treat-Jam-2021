using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_StateMachine : StateMachine
{
	NavMeshAgent _agent;
	public NavMeshAgent agent => _agent;

	protected void Awake()
	{
		States.Add(nameof(State_NPC_Idle), new State_NPC_Idle(nameof(State_NPC_Idle), this));
		States.Add(nameof(State_NPC_Move), new State_NPC_Move(nameof(State_NPC_Move), this));
		States.Add(nameof(State_NPC_Knockdown), new State_NPC_Knockdown(nameof(State_NPC_Knockdown), this));
		States.Add(nameof(State_NPC_GetUp), new State_NPC_GetUp(nameof(State_NPC_GetUp), this));
		States.Add(nameof(State_NPC_GoForCandy), new State_NPC_GoForCandy(nameof(State_NPC_GoForCandy), this));
		States.Add(nameof(State_NPC_GrabCandy), new State_NPC_GrabCandy(nameof(State_NPC_GrabCandy), this));

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

	protected override void Update()
	{
		base.Update();
	}

	protected override void SwitchState(State newState)
	{
		base.SwitchState(newState);
		myStatus.currentState = currentState.StateName;
	}

	public void Knockdown(Vector2 direction, float power, bool fear) //Feed it position and Vector
	{
		myInputs.ForceMove(direction * power);
		myInputs.ForceInput(nameof(State_NPC_Knockdown));
	}

	public void CandyPull(float pullTime, Vector2 targetPosition)
	{
		if (myStatus.GetCooldownReady("GrabbinCandy"))
			myInputs.ForceMove(targetPosition);
		myStatus.SetCooldown("GrabbinCandy", pullTime);
		myInputs.ForceInput(nameof(State_NPC_GoForCandy));
	}
}

public abstract class NPC_State : State
{
	protected NPC_StateMachine NPC_SM;

	public NPC_State(string name, NPC_StateMachine stateMachine) : base(name, stateMachine)
	{
		_stateName = name;
		NPC_SM = stateMachine;
	}
}
