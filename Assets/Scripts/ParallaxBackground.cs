using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform _cameraTransform;
    private float _lastCameraPosition;

    private void Start() 
    {
        _cameraTransform = Camera.main.transform;    
        _lastCameraPosition = Camera.main.transform.position.x;    
    }

    private void LateUpdate() 
    {
        float deltaMovement = _cameraTransform.position.x - _lastCameraPosition;  
        transform.position = new Vector2(transform.position.x + deltaMovement, transform.position.y);
        _lastCameraPosition = Camera.main.transform.position.x;  
    }
}
