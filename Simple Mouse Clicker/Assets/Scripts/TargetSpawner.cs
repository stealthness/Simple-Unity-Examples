using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    [SerializeField] private Transform upperLeftCorner;
    [SerializeField] private Transform lowerRightCorner;
    
    private HashSet<GameObject> _targets;
        
    
    private void Awake()
    {
        _targets = new HashSet<GameObject>();
        CreateTargetPool(5);
    }

    /// <summary>
    /// To ensure that GameManager constructed and available using Script Execution Order.
    /// Assume that GameManager is not null
    /// </summary>
    private void OnEnable()
    {
        GameManager.Instance.onStartGame.AddListener(StartSpawning);
    }

    // Assumed That GameManager is not null
    private void OnDisable()
    {

        GameManager.Instance.onStartGame.RemoveListener(StartSpawning);
    }

    /// <summary>
    /// Resets all targets by stopping any ongoing coroutines and deactivating all target GameObjects in the _targets HashSet.
    /// </summary>
    private void ResetTargets()
    {
        Debug.Log("TM::Reset all targets.");
        StopAllCoroutines();
        foreach (var target in _targets)
        {
            target.SetActive(false);
        }
    }

    /// <summary>
    /// Starts the spawning of targets by invoking the SpawnTarget method repeatedly at a fixed interval of 0.5 seconds.
    /// </summary>
    private void StartSpawning()
    {
        Debug.Log("TargetSpawner started spawning targets.");
        InvokeRepeating(nameof(SpawnTarget), 0f, 0.5f);
    }
    
    private void SpawnTarget()
    {
        Debug.Log("Spawning target...");
        foreach (var target in _targets.Where(target => !target.activeInHierarchy))
        {
            Debug.Log("Reusing inactive target");
            target.SetActive(true);
            target.transform.position = GetRandomVector2();
            return;
        }
        Debug.Log("No target found, creating a new target.");
        var newTarget = CreateTarget();
        newTarget.SetActive(true);
        _targets.Add(newTarget);
    }
    
    
    /// <summary>
    /// Creates a pool of target GameObjects and adds them to the _targets HashSet. The targets are instantiated at random
    /// positions within the defined area, but are initially set to inactive. The initial size of the pool can be specified
    /// with the initialSize parameter, which defaults to 5 if not provided.
    /// </summary>
    /// <param name="initialSize">The initial size of the target pool.</param>
    private void CreateTargetPool(int initialSize = 5)
    {
        for (var i = 0; i < initialSize; i++)
        {
            _targets.Add(CreateTarget());
        }
    }
        
    /// <summary>
    /// Creates a new target GameObject by instantiating the targetPrefab at a random position within the defined area.
    /// The new target is initially set to inactive and is parented to the TargetSpawner's transform. The created target
    /// is returned to the caller for further use or management.
    /// </summary>
    /// <returns>A new target GameObject.</returns>
    private GameObject CreateTarget()
    {
        var randomPosition = GetRandomVector2();
        var target = Instantiate(targetPrefab, randomPosition, Quaternion.identity, transform);
        target.SetActive(false);
        return target;
    }

    /// <summary>
    /// Generates a random Vector3 position within the defined rectangular area specified by the upperLeftCorner and
    /// lowerRightCorner Transforms. The x and y coordinates are randomly selected between the minimum and maximum
    /// values of the corners, while the z coordinate is set to 0. This method is used to determine the spawn position
    /// for targets.
    /// </summary>
    /// <returns>A random Vector3 position within the defined area.</returns>
    private Vector3 GetRandomVector2()
    {
        var maxX = upperLeftCorner.position.x;
        var minX = lowerRightCorner.position.x;
        var maxY = upperLeftCorner.position.y;
        var minY = lowerRightCorner.position.y;

        var randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        return randomPosition;
    }
}