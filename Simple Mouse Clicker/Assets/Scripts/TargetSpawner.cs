using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance { get; private set; }
    
    private HashSet<GameObject> _activeTargets = new HashSet<GameObject>();
    

    private void Awake()
    {
        Instance = this;
    }
    
    public void StartSpawning()
    {
        Debug.Log("TargetSpawner started spawning targets.");
        InvokeRepeating(nameof(SpawnTarget), 0f, 2f);
    }

    private void SpawnTarget()
    {
        Debug.Log("Target spawned.");
    }
}
