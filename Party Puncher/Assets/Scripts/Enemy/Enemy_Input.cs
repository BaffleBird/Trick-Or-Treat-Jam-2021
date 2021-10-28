using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy_Input : NPC_Input
{
    float timeBomb = 10;

    void Awake()
    {
        AddInput(nameof(State_NPC_Idle));
        AddInput(nameof(State_NPC_Move));
        AddInput(nameof(State_NPC_Leave));
        AddInput(nameof(State_NPC_Knockdown));
        AddInput(nameof(State_NPC_GoForCandy));

        AddInput(nameof(State_Enemy_Scare));
        AddInput(nameof(State_Enemy_Die));
        AddInput(nameof(State_Enemy_Sus));

        timeBomb = Random.Range(20, 30);
    }
 

    void Update()
    {
        timeBomb -= Time.deltaTime;
        decisionCounter -= Time.deltaTime;

        if (decisionCounter <= 0 && timeBomb >= 0)
        {
            float r = Random.value;
            if (r < 0.5)
                ForceInput(nameof(State_NPC_Move));
            else if (r > 0.8)
                ForceInput(nameof(State_Enemy_Sus));
            else
                ForceInput(nameof(State_NPC_Idle));
            decisionCounter = Random.Range(7f, 12f);
        }
        else if (timeBomb <= 0)
		{
            ForceInput(nameof(State_Enemy_Scare));
            timeBomb = Random.Range(20, 30);
        }
    }
}
