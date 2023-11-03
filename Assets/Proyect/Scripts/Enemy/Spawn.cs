using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<Transform> spawnPoints;

    private float _maxX;
    private float _minX;
    private float _maxZ;
    private float _minZ;

    public Spawn Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Count != 4)
        {
            Debug.LogError("spawnPoints.Length!= 4");
            return;
        }
        
        StartCoroutine(FindSpawnPoints());
    }

    /*
     * On this method, we instantiate the prefab inside the
     * square the spawn points create.
     */
    public void NewSpawn()
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomZ = Random.Range(_minZ, _maxZ);
        Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
        
        Instantiate(prefab, randomPosition, Quaternion.identity);
    }
    
    /*
     * On this coroutine, we find the min and max x and z values of the spawn points.
     */
    IEnumerator FindSpawnPoints()
    {
        /*
         * On this two lines, we take the x and z values of the spawn points
         * and store then in two lists.
         */
        List<float> spawnZ = spawnPoints.Select(x => x.position.z).ToList();
        List<float> spawnX = spawnPoints.Select(x => x.position.x).ToList();
        
        _maxZ = spawnZ.Max();
        _minZ = spawnZ.Min();
        _maxX = spawnX.Max();
        _minX = spawnX.Min();
        
        yield return null;
    }
}