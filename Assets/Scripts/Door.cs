using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float doorMovingSpeed;
    [SerializeField] Transform movingDoor;

    [SerializeField] Transform closedLocation;
    [SerializeField] Transform openLocation;

    private Vector3 doorOriginalLocation;
    private bool doorIsOpening;

    private void Start()
    {
        doorOriginalLocation = closedLocation.position;

    }

    public void OpenDoor() 
    { 
        doorIsOpening = true;
    }

    private void FixedUpdate()
    {
        if (doorIsOpening)
        {
            movingDoor.transform.position = Vector3.MoveTowards(movingDoor.transform.position,
                openLocation.position, doorMovingSpeed * Time.deltaTime);

            if (Vector3.Distance(movingDoor.transform.position, openLocation.position) < .1f)
            {
                doorIsOpening = false;
            }
                
        }
    }
}
