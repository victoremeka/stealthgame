using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 8f, turnSpeed = 10f, velocity;
    Rigidbody rigidbody;

    float targetAngle, angle;

    void Start(){
        rigidbody = GetComponent<Rigidbody> ();
        
    }
    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        float inputMagnitude = inputDirection.magnitude;
        targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, turnSpeed * Time.deltaTime * inputMagnitude);   
        velocity = inputMagnitude * speed * Time.deltaTime;
    }

    void FixedUpdate(){
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidbody.MovePosition(rigidbody.position + (velocity * transform.forward));
    }

    

}
