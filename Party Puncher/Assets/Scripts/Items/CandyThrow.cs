using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyThrow : MonoBehaviour
{
    [SerializeField] float pulseTime = 1;
	[SerializeField] int pulseCount = 3;
	[SerializeField] float pulseEffect = 2;
	[SerializeField] float range = 8;
	float timer = 0;
    int counter = 0;
	List<Collider2D> NPCs = new List<Collider2D>();
	LayerMask layerMask;
	ContactFilter2D contactFilter = new ContactFilter2D();

	private void Start()
	{
		timer = pulseTime;
		counter = pulseCount;
		layerMask = LayerMask.GetMask("Walls");
		contactFilter.SetLayerMask(LayerMask.GetMask("NPC"));
	}

	private void Update()
	{
		if (counter > 0)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				//Pulse and hit NPCs
				// Give them a timer and a move input
				// NPC Side: Check if already in state, if not, set move input. Timer set to given time plus a little buffer.
				Physics2D.OverlapCircle(transform.position, range, contactFilter, NPCs);
				TauntVisibleNPCs();
				timer = pulseTime;
				counter--;
			}
		}
		else
			Destroy(gameObject);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere((Vector2)transform.position, range);
	}

	void TauntVisibleNPCs()
	{
		for (int i = 0; i < NPCs.Count; i++)
		{
			if (NPCs[i] != null)
			{
				if(!Physics2D.Linecast(transform.position, NPCs[i].transform.position, layerMask))
				{
					NPC_StateMachine npc = NPCs[i].GetComponent<NPC_StateMachine>();
					if (npc) npc.CandyPull(pulseEffect, GetValidCandySpot());
				}
			}
		}
	}

	Vector2 GetValidCandySpot()
	{
		Vector2 candySpot = TestNavmeshThings.GetRandomMeshPoint(2.5f, transform.position);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, candySpot, 4, layerMask);
		while(hit.collider != null)
		{
			candySpot = TestNavmeshThings.GetRandomMeshPoint(2f, transform.position);
			hit = Physics2D.Raycast(transform.position, candySpot, 4, layerMask);
		}
		return candySpot;
	}
}
