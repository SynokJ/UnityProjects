using UnityEngine;

public class MinimapController : MonoBehaviour
{
    // minimap's camera folowing player position
    Transform playerPos;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 newPos = playerPos.position;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
