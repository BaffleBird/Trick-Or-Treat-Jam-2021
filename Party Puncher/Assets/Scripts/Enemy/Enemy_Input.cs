using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy_Input : NPC_Input
{
    void Awake()
    {
        AddInput(nameof(State_NPC_Idle));
        AddInput(nameof(State_NPC_Move));
        AddInput(nameof(State_NPC_Leave));
        AddInput(nameof(State_NPC_Knockdown));
        AddInput(nameof(State_NPC_GoForCandy));

        AddInput(nameof(State_Enemy_Scare));
        //Disappear
        //Suspcious
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            inputs[nameof(State_Enemy_Scare)] = true;
        }
    }
}
