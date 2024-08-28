using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Camera mainCamera;
    float cameraHeight;
    Ray ray;
    RaycastHit hitinfo;
    
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        print(mainCamera.rect);
    }
    void Update()
    {
        Physics.Raycast(mainCamera.ScreenPointToRay(player.transform.position), out hitinfo);
    }
}
