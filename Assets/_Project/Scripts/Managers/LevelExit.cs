using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "02_Caves";
    [SerializeField] private int requiredComponents = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int collected = GameManager.Instance != null ? GameManager.Instance.GetComponentsCollected() : 0;

        if (collected >= requiredComponents)
        {
            Debug.Log("[LevelExit] Requirements met. Loading scene...");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("[LevelExit] Missing ship components.");
        }
    }
}
