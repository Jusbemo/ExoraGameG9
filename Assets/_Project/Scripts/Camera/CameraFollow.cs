using UnityEngine;

/// <summary>
/// Smoothly follows a target transform with a fixed offset.
/// Runs in LateUpdate so it tracks the target after it has moved this frame.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -10f);

    [Tooltip("Approximate time (seconds) to reach the target. Lower = snappier.")]
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    public void SetTarget(Transform newTarget) => target = newTarget;

    private void Start()
    {
        // Auto-resolve the target so this prefab can be dropped into any scene
        // without wiring the reference by hand in the Inspector.
        if (target != null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            Debug.Log("[CameraFollow] Player found automatically");
        }
        else
        {
            Debug.LogWarning("[CameraFollow] No Player found in scene. Camera target remains null.");
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}
