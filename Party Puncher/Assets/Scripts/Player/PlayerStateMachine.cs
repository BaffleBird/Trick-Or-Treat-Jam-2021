using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
	protected void Awake()
	{
		States.Add(nameof(State_Player_Idle), new State_Player_Idle(nameof(State_Player_Idle), this));
		States.Add(nameof(State_Player_Move), new State_Player_Move(nameof(State_Player_Move), this)); //myStatus.SetCooldown("Jump", 0);
		States.Add(nameof(State_Player_Sprint), new State_Player_Sprint(nameof(State_Player_Sprint), this));

		_myStatus = new EntityStatus();

		currentState = States[nameof(State_Player_Idle)];
		_previousState = currentState.StateName;
		currentState.StartState();
	}

	protected override void Update()
	{
		base.Update();
		TextUpdate.Instance.SetText("State", myStatus.currentState);
		TextUpdate.Instance.SetText("Sprint", myInputs.GetInput("Sprint").ToString());
	}

	protected override void SwitchState(State newState)
	{
		base.SwitchState(newState);
		myStatus.currentState = currentState.StateName;
	}
}


public abstract class Player_State : State
{
	protected PlayerStateMachine PSM;

	public Player_State(string name, PlayerStateMachine stateMachine) : base(name, stateMachine)
	{
		_stateName = name;
		PSM = stateMachine;
	}
}