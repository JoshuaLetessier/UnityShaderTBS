using UnityEngine;

public class LockScale : MonoBehaviour
{
    private Vector3 fixedScale;

    void Start()
    {
        // Capture la scale initiale
        fixedScale = transform.localScale;
    }

    void LateUpdate()
    {
        // Réinitialise la scale après toutes les transformations
        transform.localScale = fixedScale;
    }
}
