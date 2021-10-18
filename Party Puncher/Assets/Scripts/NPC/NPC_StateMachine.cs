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

		_myStatus = new EntityStatus();

		currentState = States[nameof(State_NPC_Idle)];
		_previousState = currentState.StateName;
		currentState.StartState();

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
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
