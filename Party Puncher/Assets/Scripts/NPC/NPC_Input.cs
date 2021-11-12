using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC_Input : InputHandler
{
	protected float decisionCounter = 0;

	private void Awake()
	{
		AddInput(nameof(State_NPC_Idle));
		AddInput(nameof(State_NPC_Move));
		AddInput(nameof(State_NPC_Leave));
		AddInput(nameof(State_NPC_Knockdown));
		AddInput(nameof(State_NPC_GoForCandy));
	}

	private void OnEnable()
	{
		ForceInput(nameof(State_NPC_Move));
	}

	

	private void Update()
	{
		//Stick some simple AI stuff here
		//Run a Counter that activates a random input every once in a while?
		//Make a State transitions to a random state?

		decisionCounter -= Time.deltaTime;
		if (decisionCounter <= 0)
		{
			if (Random.value < 0.5)
				ForceInput(nameof(State_NPC_Move));
			else
				ForceInput(nameof(State_NPC_Idle));
			decisionCounter = Random.Range(8f,14f);
		}

	}
}
