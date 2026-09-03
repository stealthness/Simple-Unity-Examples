using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance { get; private set; }
    public GameObject targetPrefab;
    [SerializeField] private Transform upperLeftCorner;
    [SerializeField] private Transform lowerRightCorner;
    
    private HashSet<GameObject> _targets;
        
    
    private void Awake()
    {
        Instance = this;
        _targets = new HashSet<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            _targets.Add(CreateTarget());
        }
    }
        
    public void StartSpawning()
    {
        Debug.Log("TargetSpawner started spawning targets.");
        InvokeRepeating(nameof(SpawnTarget), 0f, 0.5f);
    }
    
    private void SpawnTarget()
    {
        foreach (var target in _targets.Where(target => !target.activeInHierarchy))
        {
            target.SetActive(true);
            return;
        }
    
        _targets.Add(CreateTarget());
    }
        
    private GameObject CreateTarget()
    {
        var maxX = upperLeftCorner.position.x;
        var minX = lowerRightCorner.position.x;
        var maxY = upperLeftCorner.position.y;
        var minY = lowerRightCorner.position.y;

        var randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        var target = Instantiate(targetPrefab, randomPosition, Quaternion.identity, transform);
        target.SetActive(false);
        return target;
    }
}