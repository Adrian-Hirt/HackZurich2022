using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SensorMovement : MonoBehaviour
{
    public InputActionReference move = null;
    public Vector2 thumbAxis;

    private void Awake()
    {
        move.action.performed += Move;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        thumbAxis = context.ReadValue<Vector2>();
        Debug.Log("Stick: " + thumbAxis.x + "/" + thumbAxis.y);
    }

    private void OnEnable()
    {
        move.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
    }
}
