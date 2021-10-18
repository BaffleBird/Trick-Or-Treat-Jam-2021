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

    public static Vector3 GetRandomPOI(float radius)
	{
        int index = Random.Range(0, POI_list.Count);
        Vector3 randomCirclePoint = Random.insideUnitCircle * radius;
        randomCirclePoint += POI_list[index].position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomCirclePoint, out hit, radius, 1))
            randomCirclePoint = hit.position;

        return randomCirclePoint;
	}    
}