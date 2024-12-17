using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Transform _transform;

    public Vector3 camPadding = new Vector3(0f,11f,-14f);

    private void Start()
    {
        _transform = transform;    
    }

    private void LateUpdate()
    {
        float z = player.position.z - camPadding.z;
        Vector3 destination = new Vector3(0f, camPadding.y, z);
        _transform.position = destination;
    }

}
