using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Player_Idle : State
{
	Vector2 currentMotion;

	public State_Player_Idle(string name, StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		currentMotion = SM.myStatus.currentMovement;
		currentMotion.y = 0;
	}

	public override void UpdateState()
	{
		if (SM.myInputs.MoveInput != Vector2.zero)
		{
			SM.SwitchState(nameof(State_Player_Move));
		}
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion.x = Mathf.Lerp(currentMotion.x, 0, 0.2f);
		currentMotion.y = Mathf.Lerp(currentMotion.y, 0, 0.2f);
		return currentMotion;
	}

	public override void EndState()
	{
		
	}
}

public class State_Player_Move : State
{
	float moveSpeed = 6f;
	Vector3 currentMotion;

	public State_Player_Move(string name, StateMachine stateMachine) : base(name, stateMachine) { }

	
	public override void StartState()
	{
		currentMotion = SM.myStatus.currentMovement;
	}

	public override void UpdateState()
	{
		if (SM.myInputs.GetInput("Sprint") && SM.myInputs.MoveInput != Vector2.zero)
			SM.SwitchState(nameof(State_Player_Sprint));
		else if (SM.myInputs.MoveInput == Vector2.zero)
			SM.SwitchState(nameof(State_Player_Idle));
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion = Vector2.Lerp(currentMotion, SM.myInputs.MoveInput * moveSpeed, 0.25f);

		return currentMotion;
	}

	public override void EndState()
	{
		
	}
}

public class State_Player_Sprint : State
{
	float sprintSpeed = 10f;
	Vector3 currentMotion;

	public State_Player_Sprint(string name, StateMachine stateMachine) : base(name, stateMachine) { }

	public override void StartState()
	{
		currentMotion = SM.myStatus.currentMovement;
	}

	public override void UpdateState()
	{
		if (!SM.myInputs.GetInput("Sprint") && SM.myInputs.MoveInput != Vector2.zero)
			SM.SwitchState(nameof(State_Player_Move));
		else if (SM.myInputs.MoveInput == Vector2.zero)
			SM.SwitchState(nameof(State_Player_Idle));
	}

	public override Vector2 MotionUpdate()
	{
		currentMotion = Vector2.Lerp(currentMotion, SM.myInputs.MoveInput * sprintSpeed, 0.25f);

		return currentMotion;
	}

	public override void EndState()
	{
	}
}
