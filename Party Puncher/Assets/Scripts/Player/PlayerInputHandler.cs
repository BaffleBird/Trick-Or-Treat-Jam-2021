using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : InputHandler
{
	PlayerControls playerControls;
	public PlayerControls PlayerControl => playerControls;


	private void Awake()
	{
		playerControls = new PlayerControls();

		inputs.Add("Sprint", false);
		inputs.Add("Attack", false);

		playerControls.PlayerInGameButtons.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
		playerControls.PlayerInGameButtons.Move.canceled += ctx => moveInput = ctx.ReadValue<Vector2>();

		playerControls.PlayerInGameButtons.Sprint.performed += ctx => inputs["Sprint"] = true;
		playerControls.PlayerInGameButtons.Sprint.canceled += ctx => inputs["Sprint"] = false;

		playerControls.PlayerInGameButtons.Attack.started += ctx => inputs["Attack"] = true;
		playerControls.PlayerInGameButtons.AttackHold.performed += ctx => inputs["AttackHold"] = true;
		playerControls.PlayerInGameButtons.AttackHold.canceled += ctx => inputs["AttackHold"] = false;
		playerControls.PlayerInGameButtons.Attack.canceled += ctx => inputs["AttackHold"] = false;

	}

	private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}
}
