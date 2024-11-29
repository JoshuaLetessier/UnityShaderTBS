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
        // R�initialise la scale apr�s toutes les transformations
        transform.localScale = fixedScale;
    }
}
