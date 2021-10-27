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

    [SerializeField] GameObject[] NPC_prefabs;
    public List<GameObject> pooledNPCs;
    public int maxNPCs;
    public int startingNPCs;

    [SerializeField] GameObject[] Enemy_prefabs;
    public List<GameObject> pooledEnemies;
    int maxEnemies;
    public float npcsPerEnemy;

    [SerializeField] float npcTick = 12;
    float npcTimer;
    [SerializeField] float enemyTick = 10;
    float enemyTimer;

    void Start()
    {
        SpawnArea = GetComponent<CircleCollider2D>();
        pooledNPCs = new List<GameObject>();

        
        GameObject tmp;
        for (int i = 0; i < maxNPCs; i++)
        {
            int n = Random.Range(0, NPC_prefabs.Length);
            tmp = Instantiate(NPC_prefabs[n]);
            tmp.SetActive(false);
            pooledNPCs.Add(tmp);
        }

        if (startingNPCs > maxNPCs) startingNPCs = maxNPCs;
        if (startingNPCs < 0) startingNPCs = 0;

        maxEnemies = (int)(maxNPCs / npcsPerEnemy);
        for (int i = 0; i < maxEnemies; i++)
        {
            int n = Random.Range(0, Enemy_prefabs.Length);
            tmp = Instantiate(Enemy_prefabs[n]);
            tmp.SetActive(false);
            pooledEnemies.Add(tmp);
        }

        SpawnStartingNPCs();

        npcTimer = npcTick;
        enemyTimer = enemyTick;
    }

    
	private void Update()
	{
        npcTimer -= Time.deltaTime;
        if (npcTimer <= 0)
		{
            SpawnNewNPC();
            
            npcTimer = npcTick;
		}

        enemyTimer -= Time.deltaTime;
        if (enemyTimer <= 0)
		{
            int i = (GameManager.instance.dataSystem.npcCount / maxEnemies);
            if (GameManager.instance.dataSystem.enemyCount < maxEnemies)
			{
                SpawnNewEnemy();
			}
            enemyTimer = enemyTick - (GameManager.instance.dataSystem.npcCount * 0.05f);
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
        Vector3 possiblePosition = new Vector3();
        bool locationFound = false;
        while(!locationFound)
		{
            possiblePosition = TestNavmeshThings.GetRandomPOI(5);

            Vector3 viewPos = Camera.main.WorldToViewportPoint(possiblePosition);
            if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                // Your object is in the range of the camera, you can apply your behaviour
                locationFound = true;
            }
        }
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
