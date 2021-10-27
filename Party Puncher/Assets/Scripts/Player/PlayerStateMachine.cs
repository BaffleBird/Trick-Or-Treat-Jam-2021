using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
	[SerializeField] GameObject _candyPrefab;
	public GameObject candyPrefab => _candyPrefab;

	[SerializeField] ParticleSystem _candyParticles;
	public ParticleSystem candyParticles => _candyParticles;

	[SerializeField] SpriteRenderer _aimSprite;
	public SpriteRenderer aimSprite => _aimSprite;

	protected void Awake()
	{
		States.Add(nameof(State_Player_Idle), new State_Player_Idle(nameof(State_Player_Idle), this));
		States.Add(nameof(State_Player_Move), new State_Player_Move(nameof(State_Player_Move), this)); //myStatus.SetCooldown("Jump", 0);
		States.Add(nameof(State_Player_Sprint), new State_Player_Sprint(nameof(State_Player_Sprint), this));
		States.Add(nameof(State_Player_ReadyCandy), new State_Player_ReadyCandy(nameof(State_Player_ReadyCandy), this));
		States.Add(nameof(State_Player_ThrowCandy), new State_Player_ThrowCandy(nameof(State_Player_ThrowCandy), this));
		States.Add(nameof(State_Player_Shove), new State_Player_Shove(nameof(State_Player_Shove), this));
		States.Add(nameof(State_Player_Windup), new State_Player_Windup(nameof(State_Player_Windup), this));
		States.Add(nameof(State_Player_Punch), new State_Player_Punch(nameof(State_Player_Punch), this));
		States.Add(nameof(State_Player_Kick), new State_Player_Kick(nameof(State_Player_Kick), this));

		_myStatus = new EntityStatus();

		currentState = States[nameof(State_Player_Idle)];
		_previousState = currentState.StateName;
		currentState.StartState();
	}

	protected override void Update()
	{
		base.Update();
		//TextUpdate.Instance.SetText("State", myStatus.currentState);
	}

	protected override void SwitchState(State newState)
	{
		base.SwitchState(newState);
		myStatus.currentState = currentState.StateName;
	}

	protected override void OnDrawGizmos()
	{
		if (currentState != null)
			currentState.TestUpdate();
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