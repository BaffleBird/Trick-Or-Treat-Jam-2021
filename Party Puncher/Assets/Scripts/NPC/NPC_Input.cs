using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC_Input : InputHandler
{
	private void Awake()
	{
		AddInput(nameof(State_NPC_Idle));
		AddInput(nameof(State_NPC_Move));
		//inputs.Add("Follow", false);
		//inputs.Add("Flee", false);
		AddInput(nameof(State_NPC_Knockdown));
		AddInput(nameof(State_NPC_GoForCandy));
	}

	private void Update()
	{
		//Stick some simple AI stuff here
		if(Keyboard.current.qKey.wasPressedThisFrame)
		{
			inputs[nameof(State_NPC_Move)] = true;
		}
	}
}
