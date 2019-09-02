using UnityEngine;

public class BarPositioning : MonoBehaviour
{
    void Update()
    {
        var camera = Camera.main;
        var position = camera.transform.position;
        var newPosition = new Vector3(position.x - 12, position.y + 7, 0);
        transform.position = newPosition;
    }
}
