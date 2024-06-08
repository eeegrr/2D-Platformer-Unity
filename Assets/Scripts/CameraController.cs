using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // get the reference to players transform
    [SerializeField] Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        //get players x and y
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
    }
}
