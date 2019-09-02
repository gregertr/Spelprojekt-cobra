using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    Quaternion rotation;

    void Awake()
    {
        rotation = new Quaternion(0, 0, 0, 1);
    }

    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
