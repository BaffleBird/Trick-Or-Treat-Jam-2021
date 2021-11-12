using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_StateMachine : NPC_StateMachine
{
	void Awake()
	{
		States.Add(nameof(State_NPC_Idle), new State_Enemy_Idle(nameof(State_NPC_Idle), this));
		States.Add(nameof(State_NPC_Move), new State_Enemy_Move(nameof(State_NPC_Move), this));
		States.Add(nameof(State_NPC_Knockdown), new State_Enemy_Knockdown(nameof(State_NPC_Knockdown), this));
		States.Add(nameof(State_NPC_GetUp), new State_NPC_GetUp(nameof(State_NPC_GetUp), this));
		States.Add(nameof(State_NPC_Leave), new State_Enemy_Leave(nameof(State_NPC_Leave), this));
		States.Add(nameof(State_NPC_GoForCandy), new State_NPC_GoForCandy(nameof(State_NPC_GoForCandy), this));
		States.Add(nameof(State_NPC_GrabCandy), new State_NPC_GrabCandy(nameof(State_NPC_GrabCandy), this));

		States.Add(nameof(State_Enemy_Scare), new State_Enemy_Scare(nameof(State_Enemy_Scare), this));
		States.Add(nameof(State_Enemy_Die), new State_Enemy_Die(nameof(State_Enemy_Die), this));
		States.Add(nameof(State_Enemy_Sus), new State_Enemy_Sus(nameof(State_Enemy_Sus), this));

		_myStatus = new EntityStatus();

		currentState = States[nameof(State_NPC_Idle)];
		_previousState = currentState.StateName;
		currentState.StartState();

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
		_agent.speed = Random.Range(2, 3.5f);
		_agent.SetDestination(transform.position);

		int r = Random.Range(0, 3);
		Color c = Color.white;
		switch (r)
		{
			case 0:
				c.a = 0.85f;
				_mySprite.color = c;
				break;
			case 1:
				myShadow.color = new Color(0, 0, 0, 0);
				break;
			case 2:
				c = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
				_mySprite.color = Color.Lerp(_mySprite.color, c, 0.3f);
				break;
		}
	}

	public override void ResetNPC()
	{
		base.ResetNPC();
		mySprite.color = Color.white;
		myShadow.color = new Color(0, 0, 0, 0.92f);

		int r = Random.Range(0, 3);
		Color c = Color.white;
		switch (r)
		{
			case 0:
				c.a = 0.8f;
				_mySprite.color = c;
				break;
			case 1:
				myShadow.color = new Color(0, 0, 0, 0);
				break;
			case 2:
				c = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
				_mySprite.color = Color.Lerp(_mySprite.color, c, 0.3f);
				break;
		}
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
		if (power > 15)
			Die();
		else
			Flee();
	}

	public override void CandyPull(float pullTime, Vector2 targetPosition) //Don't grab the candy.
	{
		return;
	}

	public void Die()
	{
		myInputs.ForceInput(nameof(State_Enemy_Die));
	}
}
