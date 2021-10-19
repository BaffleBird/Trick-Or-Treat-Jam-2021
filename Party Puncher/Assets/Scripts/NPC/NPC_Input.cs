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
		//inputs.Add("PickUp", false);
		AddInput(nameof(State_NPC_Knockdown));
		//inputs.Add("KnockOut", false);
		//inputs.Add("GetUp", false);
	}

	private void Update()
	{
		if(Keyboard.current.qKey.wasPressedThisFrame)
		{
			inputs[nameof(State_NPC_Move)] = true;
		}
	}
}
