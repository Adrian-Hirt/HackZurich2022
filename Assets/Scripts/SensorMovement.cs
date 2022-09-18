using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SensorMovement : MonoBehaviour
{
    public GameObject prismaCube = null;
    public GameObject cameraObj = null;
    public GameObject laserObj = null;
    public GameObject hiddenLaserObj = null;
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

    private void FixedUpdate()
    {
        if (true)
        {
            hiddenLaserObj.transform.LookAt(prismaCube.transform);

            transform.rotation = Quaternion.Euler(0, hiddenLaserObj.transform.eulerAngles.y, 0);
            cameraObj.transform.localRotation = Quaternion.Euler(hiddenLaserObj.transform.eulerAngles.x + 90, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        thumbAxis = context.ReadValue<Vector2>();
        transform.Rotate(0, thumbAxis.x, 0);
        cameraObj.transform.Rotate(-thumbAxis.y, 0, 0);
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
