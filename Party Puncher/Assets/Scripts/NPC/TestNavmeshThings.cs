using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavmeshThings : MonoBehaviour
{
    [SerializeField] GameObject POI_Parent;
    [HideInInspector] public static List<Transform> POI_list = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
		{
            POI_list.Add(child);
		}
    }

    public static Vector3 GetRandomMeshPoint(float radius, Vector3 position)
    {
        Vector3 randomCirclePoint = Random.insideUnitCircle * radius;
        randomCirclePoint += position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomCirclePoint, out hit, radius, 1))
            randomCirclePoint = hit.position;

        return randomCirclePoint;
    }

    public static Vector3 GetRandomPOI(float radius)
	{
        int index = Random.Range(0, POI_list.Count);
        return GetRandomMeshPoint(radius, POI_list[index].position);
	}

    public static Vector3 GetRandomFurthestPOI(float radius, Vector3 position)
	{
        Transform furthestPOI = POI_list[0];
        float distance = Vector3.Distance(position, furthestPOI.position);
        for (int i = 0; i < POI_list.Count; i++)
		{
            float newDistance = Vector3.Distance(position, POI_list[i].position);

            if (newDistance > distance && Random.value < 0.5f)
			{
                furthestPOI = POI_list[i];
                distance = newDistance;
			}
        }

        return GetRandomMeshPoint(radius, furthestPOI.position);
    }

    public static Vector3 GetFleeablePOI(float radius, Vector3 position, Vector3 avoidPosition)
	{
        Transform FleeablePOI = POI_list[0];
        Vector3 directionToAvoid = (avoidPosition - position).normalized;
        for (int i = 0; i < POI_list.Count; i++)
        {
            Vector3 directionToFlee = POI_list[i].position;
            if(Vector3.Angle(directionToAvoid, directionToFlee) > 30)
			{
                FleeablePOI = POI_list[i];
			}
        }
        ShufflePOI();
        return GetRandomMeshPoint(radius, FleeablePOI.position);
    }

    public static void ShufflePOI()
	{
        for(int i = 0; i < POI_list.Count-1; i++)
		{
            int r = Random.Range(i, POI_list.Count);
            Transform temp = POI_list[i];
            POI_list[i] = POI_list[r];
            POI_list[r] = temp;
		}
	}
}
