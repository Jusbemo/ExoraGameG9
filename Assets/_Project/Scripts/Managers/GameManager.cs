using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int componentsCollected = 0;
    public int totalComponents = 3;
    public string currentObjective = "Objetivo: reparar la nave";

    public Vector3 lastCheckpointPosition;
    public bool hasActiveCheckpoint = false;
    public Transform initialSpawnPoint;

    public event Action OnComponentCollected;
    public event Action<string> OnObjectiveChanged;
    public event Action<Vector3> OnCheckpointActivated;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // GameManager lives under the _Managers organizational parent, so persist
        // that root object rather than this GameObject (DontDestroyOnLoad requires a root).
        DontDestroyOnLoad(transform.root.gameObject);
    }

    public void AddComponent()
    {
        componentsCollected++;
        OnComponentCollected?.Invoke();
    }

    public void SetObjective(string text)
    {
        currentObjective = text;
        OnObjectiveChanged?.Invoke(currentObjective);
    }

    public int GetComponentsCollected()
    {
        return componentsCollected;
    }

    public int GetTotalComponents()
    {
        return totalComponents;
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        hasActiveCheckpoint = true;
        Debug.Log($"[GameManager] Checkpoint set at {position}");
        OnCheckpointActivated?.Invoke(position);
    }

    public Vector3 GetRespawnPosition()
    {
        if (hasActiveCheckpoint)
        {
            return lastCheckpointPosition;
        }

        if (initialSpawnPoint != null)
        {
            return initialSpawnPoint.position;
        }

        Debug.LogWarning("[GameManager] No spawn point assigned, using origin");
        return Vector3.zero;
    }

    public void ResetCheckpoints()
    {
        hasActiveCheckpoint = false;
    }
}
