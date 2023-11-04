using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private List<Transform> spawnPoints;

    private static int _numbersSpawnSkeleton;
    private static int _numbersSpawnPotion;
    private static float _time;
    private static float _incrementSpawn;
    
    private float _maxX;
    private float _minX;
    private float _maxZ;
    private float _minZ;

    private bool _canSpawnSkeleton = true;
    private bool _canSpawnPotion = true;
    
    void Start()
    {
        if (spawnPoints.Count != 4)
        {
            Debug.LogError("spawnPoints.Length!= 4");
            return;
        }
        StartCoroutine(FindSpawnPoints());
    }

    void Update()
    {
        if (!PlayerController.CanSpawnEnemies)
        {
            return;
        }
        AddTime(Time.deltaTime);
        if (prefabs.Count == 1) // Spawn Skeleton
        {
            if(_numbersSpawnSkeleton == 0 && _canSpawnSkeleton)
            {
                StartCoroutine(SpawnWithDelaySkeleton(5, 9 + Mathf.FloorToInt(Mathf.Pow(10f, _incrementSpawn))));
                IncrementSpawnNumber();
            }
        }
        else if (_numbersSpawnPotion < 5 && _canSpawnPotion)
        {
            StartCoroutine(SpawnWithDelayPotion(5));
        }   
    }

    /*
     * On this method, we add the time of Time.deltaTime to
     * the variable _time just to get the time in seconds that
     * has elapsed since the init of the game.
     */
    private static void AddTime(float time)
    {
        _time += time;
    }

    /*
     * On this method, the number of spawn points is incremented so that
     * give us the information of the number of spawns we have in the
     * scene. 
     */
    private static void AddNewSpawnSkeleton()
    {
        _numbersSpawnSkeleton++;
    }
    
    private static void AddNewSpawnPotion()
    {
        _numbersSpawnPotion++;
    }

    /*
     * On this method, the namber of the exponent to generate the
     * number of Skeletons is incremented in 0.2. 
     */
    private static void IncrementSpawnNumber()
    {
        _incrementSpawn += 0.2f;
    }

    /*
     * On this coroutine, we wait for a given time and then spawn a prefab.
     *
     * If the prefab is a Skeleton, the numberSpawn value is gonna be
     * always 10, otherwise it's gonna be 1 cause it will be potion.
     *
     * As well, we increment the number of spawns we have in the scene.
     */
    private IEnumerator SpawnWithDelaySkeleton(int time, int numberSpawn)
    {
        _canSpawnSkeleton = false;

        yield return new WaitForSeconds(time);
        for (var x = 0; x < numberSpawn; x++)
        {
            NewSpawnSkeleton();
            AddNewSpawnSkeleton();
        }

        _canSpawnSkeleton = true;
    }
    private IEnumerator SpawnWithDelayPotion(int time)
    {
        _canSpawnPotion = false;
        NewSpawnPotion();
        AddNewSpawnPotion();
        yield return new WaitForSeconds(time);
        
        _canSpawnPotion = true;
    }
    /*
     * On this method, we instantiate the prefab inside the
     * square the spawn points create.
     */
    public void NewSpawnSkeleton()
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomZ = Random.Range(_minZ, _maxZ);
        Vector3 randomPosition = new Vector3(randomX, 1.57f, randomZ);
        
        Instantiate(prefabs[0], randomPosition, Quaternion.identity);
    }

    public void NewSpawnPotion()
    {
        float randomX = Random.Range(_minX, _maxX);
        float randomZ = Random.Range(_minZ, _maxZ);
        Vector3 randomPosition = new Vector3(randomX, 3.89f, randomZ);
        int randomPrefabIndex = Random.Range(0, prefabs.Count);
        
        Instantiate(prefabs[randomPrefabIndex], randomPosition, Quaternion.identity);
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