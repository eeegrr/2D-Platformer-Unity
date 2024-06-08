using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Transform movingPlatform;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;

    [SerializeField] float moveSpeed = 5.0f;

    private bool movingRight;

    private void FixedUpdate()
    {
        if(movingRight)
        {
            MovePlatform(rightPoint);
        }
        else
        {
            MovePlatform(leftPoint);
        }
        
    }

    private void MovePlatform(Transform target)
    {
        movingPlatform.transform.position = Vector3.MoveTowards(movingPlatform.transform.position, 
            target.transform.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(movingPlatform.transform.position, target.position) < 0.1f)
        {
            movingRight = !movingRight; // true > false, false > treue
        }
    }
}
