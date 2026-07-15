using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int componentsCollected = 0;
    public int totalComponents = 3;
    public string currentObjective = "Objetivo: reparar la nave";

    public event Action OnComponentCollected;
    public event Action<string> OnObjectiveChanged;

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
}
