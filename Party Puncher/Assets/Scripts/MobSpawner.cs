using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] GameObject[] NPC_prefabs;
    public List<GameObject> pooledNPCs;
    public int maxNPCs;

    [SerializeField] GameObject[] Enemy_prefabs;
    public List<GameObject> pooledEnemies;
    public int maxEnemies;

    [SerializeField] float enemyTick = 10;
    float enemyTimer;

    void Start()
    {
        SpawnArea = GetComponent<CircleCollider2D>();
        pooledNPCs = new List<GameObject>();

        int n = 0;
        GameObject tmp;
        for (int i = 0; i < maxNPCs; i++)
        {
            tmp = Instantiate(NPC_prefabs[n]);
            tmp.SetActive(false);
            pooledNPCs.Add(tmp);
            n++;
            if (n >= NPC_prefabs.Length) n = 0;
        }

        n = 0;
        for (int i = 0; i < maxEnemies; i++)
        {
            tmp = Instantiate(Enemy_prefabs[n]);
            tmp.SetActive(false);
            pooledEnemies.Add(tmp);
            n++;
            if (n >= NPC_prefabs.Length) n = 0;
        }

        SpawnStartingNPCs();
        enemyTimer = enemyTick;
    }

	private void Update()
	{
        enemyTimer -= Time.deltaTime;
        if (enemyTimer <= 0)
		{
            int i = (GameManager.instance.dataSystem.npcCount / maxEnemies);
            if (GameManager.instance.dataSystem.enemyCount < maxEnemies)
			{
                SpawnNewEnemy();
			}
            enemyTimer = enemyTick - (GameManager.instance.dataSystem.npcCount * 0.005f);
		}
	}

	public GameObject GetNPC()
    {
        for (int i = 0; i < pooledNPCs.Count; i++)
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

    public GameObject GetEnemy()
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                NPC_StateMachine tmp = pooledEnemies[i].GetComponent<NPC_StateMachine>();
                tmp.ResetNPC();
                return pooledEnemies[i];
            }
        }
        return null;
    }

    //Spawn the amount to start with and put each one in a random spot
    public void SpawnStartingNPCs()
	{
        for (int i = 0; i < pooledNPCs.Count; i++)
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

                GameManager.instance.dataSystem.npcCount++;
            }
        }

        GameObject enemy = GetEnemy();
        if (enemy != null)
		{
            enemy.transform.position = TestNavmeshThings.GetRandomPOI(5);
            NPC_StateMachine tmp = enemy.GetComponent<NPC_StateMachine>();

            enemy.SetActive(true);
            tmp.ResetNPC();

            GameManager.instance.dataSystem.enemyCount++;
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

            GameManager.instance.dataSystem.npcCount++;
        }
    }

    public void SpawnNewEnemy()
	{
        //Spawn an enemy somewhere out of sight
        Vector3 possiblePosition = TestNavmeshThings.GetRandomFurthestPOI(6, GameObject.FindWithTag("Player").transform.position);
        GameObject enemy = GetEnemy();
        if (enemy != null)
		{
            enemy.transform.position = possiblePosition;
            NPC_StateMachine tmp = enemy.GetComponent<NPC_StateMachine>();

            tmp.ResetNPC();
            enemy.SetActive(true);

            GameManager.instance.dataSystem.enemyCount++;
        }
    }
}
