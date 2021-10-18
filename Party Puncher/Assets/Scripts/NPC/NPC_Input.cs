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
		//inputs.Add("KnockDown", false);
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

	//Mess with Avoidance Priority on
	//Behaviour ScriptableObjects for Flocking: public Vector3 CalculateMove() using Flock Agent, Context, and Flock

	//Agent Class has a Collider and a 'Move' function. We can just set a target position as movement and the NavMesh Agent will move according to its settings, has it's own avoidance area too

	//Flock Class:
	//Has a Flock Agent Prefab
	//a list of Agents
	//Slot for Behaviour SO. Specifically one Composite Behaviour.
	//Has a circle for spawn and local
	//Has a Count for Population
	//Has a density modifier
	//Drive Factor (Speed Multiplier) Cuz it's usually a direction
	//Max Speed to Cap Drife Facter
	//Neighbor Radius
	//Avoidance Radius Multiplier (0 to 1 as a mod to NeighborRadius)

	//SqrMaxSpeed, SqrNeighborRadius, SqrAvoidance(*SqrNeighborRadius)

	//Spawn a bunch of agents in random circle location * count * density
	// Name em
	// Add em to a string
}
