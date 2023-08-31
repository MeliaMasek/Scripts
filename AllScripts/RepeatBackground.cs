using System.Collections.Generic;
using UnityEngine;
// code borrowed and modified from N3K EN on youtube https://www.youtube.com/watch?v=HIsEqKPoJXM
// code borrowed and modified from N3K EN on youtube https://www.youtube.com/watch?v=WnvW6m4Fqmo&list=PLLH3mUGkfFCXps_IYvtPcE9vcvqmGMpRK&index=8
public class RepeatBackground : MonoBehaviour
{
    public GameObject[] floorPrefab;
    private Transform playerTransform;
    private List<GameObject> activeTiles;
    
    public float floorLength = 10f;
    public int floorAmount = 2;
    public float safeZone = 10f;

    private float spawnZ = -5.5f;
    private int lastPrefabIndex = 0;
   
    public void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - floorAmount * floorLength))
        {
            SpawnFloor();
            DeleteFloor();
        }
    }

    public void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < floorAmount; i++)
        {
            if (i < 2)
            {
                SpawnFloor(0);
            }
            else
            {
                SpawnFloor();
            }
        }
    }

    public void SpawnFloor(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(floorPrefab[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(floorPrefab[prefabIndex] as GameObject);
        }
        go.transform.SetParent(transform);
        go.transform.position =Vector3.forward * spawnZ;
        spawnZ += floorLength;
        activeTiles.Add(go);
    }

    public void DeleteFloor()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (floorPrefab.Length <= 1)
            return 0;
        int randonIndex = lastPrefabIndex;
        while (randonIndex == lastPrefabIndex)
        {
            randonIndex = Random.Range(0, floorPrefab.Length);
        }

        lastPrefabIndex = randonIndex;
        return randonIndex;
    }
}
