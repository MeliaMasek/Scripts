using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject[] enemy;
    public float spawnTime = .5f;
    private Vector3 spawnPosition;
 
    void Start () 
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }
 
    void Spawn ()
    {
        spawnPosition.x = Random.Range (-9, 9);
        spawnPosition.y = 0.25f;
        spawnPosition.z = Random.Range (0, 22);
 
        Instantiate(enemy[UnityEngine.Random.Range(0, enemy.Length - 1)], spawnPosition, Quaternion.identity);
    }
}
