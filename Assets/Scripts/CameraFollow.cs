using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float threshold = 3f, moveSpeed = .5f, smoothMoveSpeed = .3f, smoothTime = .1f;
    float velocity;
    void Update()
    {
        moveSpeed = Mathf.SmoothDamp(smoothMoveSpeed, moveSpeed, ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.MoveTowards(transform.position.z, player.transform.position.z, moveSpeed));
    }
}
