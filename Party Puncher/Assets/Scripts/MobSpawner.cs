using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobSpawner : MonoBehaviour
{
    #region Singleton
    private static MobSpawner _instance;
    public static MobSpawner instance => _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion
    CircleCollider2D SpawnArea;

    [SerializeField] GameObject NPC_prefab;
    public List<GameObject> pooledNPCs;
    public int maxNPCs;
    public int startingNPCs;
    public float spawnRate;

    //[SerializeField] GameObject Enemyprefab;

    void Start()
    {
        SpawnArea = GetComponent<CircleCollider2D>();
        pooledNPCs = new List<GameObject>();

        GameObject tmp;
        for (int i = 0; i < maxNPCs; i++)
        {
            tmp = Instantiate(NPC_prefab);
            tmp.SetActive(false);
            pooledNPCs.Add(tmp);
        }

        if (startingNPCs > maxNPCs) startingNPCs = maxNPCs;
        if (startingNPCs < 0) startingNPCs = 0;

        SpawnStartingNPCs();
    }

	private void Update()
	{
		//Based on Spawn rate spawn a new guy every one in a while
	}

	public GameObject GetNPC()
    {
        for (int i = 0; i < maxNPCs; i++)
        {
            if (!pooledNPCs[i].activeInHierarchy)
			{
                NPC_StateMachine tmp = pooledNPCs[i].GetComponent<NPC_StateMachine>();
                tmp.ResetNPC();
				return pooledNPCs[i];
            }
        }
        return null;
    }

    //Spawn the amount to start with and put each one in a random spot
    public void SpawnStartingNPCs()
	{
        for (int i = 0; i < startingNPCs; i++)
        {
            GameObject npc = GetNPC();
            if (npc == null)
			{
                break;
			}
            else
			{
                npc.transform.position = TestNavmeshThings.GetRandomPOI(5);
                NPC_StateMachine tmp = npc.GetComponent<NPC_StateMachine>();
                npc.SetActive(true);
                tmp.ResetNPC();
            }
        }
    }

    //Spawn an npc in the starting zone
    public void SpawnNewNPC()
	{
        GameObject npc = GetNPC();
        if (npc != null)
        {
            npc.transform.position = TestNavmeshThings.GetRandomMeshPoint(SpawnArea.radius, transform.position);

            NPC_StateMachine tmp = npc.GetComponent<NPC_StateMachine>();
            tmp.ResetNPC();

            npc.SetActive(true);
        }
    }
}
