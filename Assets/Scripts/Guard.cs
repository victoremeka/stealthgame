using System;
using System.Collections;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform path;
    public float waitTime = .5f, moveSpeed = 5f, turnSpeed = 6f, viewDistance = 10, angleToPlayer;
    Light spotlight;
    public LayerMask layerMask;
    Ray ray;
    Vector3 playerDisplacement;
    Transform player;
    Color originalColor;
    public event Action FoundPlayer;

    

    bool CanSeePlayer(){
        playerDisplacement = player.position - transform.position;
        angleToPlayer = Mathf.Atan2(playerDisplacement.x, playerDisplacement.z) * Mathf.Rad2Deg;  // World coords.
        
        if (playerDisplacement.magnitude <= viewDistance && Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y,angleToPlayer)) <= spotlight.spotAngle/2)
        {
            ray = new Ray(transform.position, playerDisplacement.normalized);
            Debug.DrawLine(transform.position, transform.position + playerDisplacement.normalized * viewDistance, Color.yellow);
            if(!Physics.Raycast(ray, viewDistance, layerMask)){ // Account for obstacles
                return true;
            }
        }
        
        return false;
    }

    void Start()
    {
        spotlight = GetComponentInChildren<Light>();
        player = FindObjectOfType<Player>().transform;
        originalColor = spotlight.color;
        StartCoroutine(FollowPath(moveSpeed));
        StartCoroutine(LightStatus());
    }

    void Update()
    {
         
    }

    IEnumerator LightStatus(){
        while (true){
            if (CanSeePlayer())
            {
                spotlight.color = Color.red;
                yield return new WaitForSeconds(.3f);
                FoundPlayer?.Invoke();
            } else
            {
                spotlight.color = originalColor;
            }
            
            yield return null;
        }
    }
        

    IEnumerator FollowPath(float moveSpeed){
        int index = 0;
        Vector3 destination = path.GetChild(index).position;

        yield return StartCoroutine(LookTowards(destination));
        

        while(true)
        {

            if (transform.position.x != destination.x || transform.position.z != destination.z) 
            {
                transform.position =  Vector3.MoveTowards(transform.position, new Vector3(destination.x, transform.position.y, destination.z), moveSpeed*Time.deltaTime);
                yield return null;
                continue;
            }

            index = (index+1) % path.childCount;
            
            destination = path.GetChild(index).position;
            
            yield return StartCoroutine(LookTowards(destination));
            yield return new WaitForSeconds(waitTime);
            
        }
    }

    IEnumerator LookTowards(Vector3 destination){
        Vector3 target = (destination - transform.position).normalized;
        float angle = (float)Math.Atan2(target.x, target.z) * Mathf.Rad2Deg;
        
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, angle)) > 0.05f)
        {
            transform.eulerAngles = Mathf.MoveTowardsAngle(transform.eulerAngles.y, angle, turnSpeed) * Vector3.up;
            yield return null;
        }
    }

    void OnDrawGizmos(){
        Vector3 currentPos =  path.GetChild(0).position;
        foreach (Transform waypoint in path)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(currentPos,waypoint.position);
            currentPos  = waypoint.position;
        }
        Gizmos.DrawLine(currentPos, path.GetChild(0).position);
    }
}
