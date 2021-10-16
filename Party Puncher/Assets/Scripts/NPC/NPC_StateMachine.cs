using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_StateMachine : StateMachine
{
	[SerializeField] Transform _target;
	public Transform target => _target;

	NavMeshAgent agent;

	protected void Awake()
	{
		//currentState = States[nameof(State_Player_Idle)];
		_previousState = currentState.StateName;
		currentState.StartState();
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
