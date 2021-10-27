using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPickup : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameManager.instance.dataSystem.candyCount++;
		Destroy(gameObject);
	}
}
