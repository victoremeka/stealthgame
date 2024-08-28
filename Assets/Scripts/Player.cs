using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 8f, turnSpeed = 10f;
    public Vector3 velocity;
    Rigidbody playerRigidbody;
    float smoothInputMagnitude, smoothMoveVelocity, smoothMoveTime = .1f;

    float targetAngle, angle;

    void Start(){
        playerRigidbody = GetComponent<Rigidbody> ();
        
    }
    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp (smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, turnSpeed * Time.deltaTime * inputMagnitude);   
        velocity = smoothInputMagnitude * speed * transform.forward;
    }

    void FixedUpdate(){
        playerRigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        playerRigidbody.MovePosition(playerRigidbody.position + velocity * Time.deltaTime);
    }

    

}
