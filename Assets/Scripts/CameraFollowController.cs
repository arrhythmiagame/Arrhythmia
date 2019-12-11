using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float moveSpeed;
    [SerializeField] CinemachineVirtualCamera theVirtualCamera;
    [SerializeField] float minDistance = 0.1f;

    [Header("Debug Info")]
    [SerializeField] Vector3 distance;
    [SerializeField] float absDistX;
    [SerializeField] float absDistY;
    private Vector3 targetPos;


    void Update()
    {
        if (followTarget != null)
        {
            targetPos = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
            distance = (targetPos - transform.position);
            Vector3 velocity = distance * moveSpeed;
            absDistX = Mathf.Abs(distance.x);
            absDistY = Mathf.Abs(distance.y);
            if (absDistX < minDistance && absDistY < minDistance)
            {
                theVirtualCamera.Follow = followTarget;
            } else
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, Time.deltaTime);
            }

        }
    }
}
