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
    public InputActionReference moveRight = null;
    public InputActionReference moveLeft = null;
    public InputActionReference secondary = null;
    public Vector2 thumbAxis;

    public GameObject screenText = null;

    private bool followPrismRod = false;

    private void Awake()
    {
        moveRight.action.performed += MoveRight;
        moveLeft.action.performed += MoveLeft;
        secondary.action.performed += SecondaryPressed;
    }

    private void FixedUpdate()
    {
        // Set screen text
        var laserOrigin = laserObj.transform.position;
        var prismaOrigin = prismaCube.transform.position;
        var distance = Vector2.Distance(new Vector2(laserOrigin.x, laserOrigin.z),
            new Vector2(prismaOrigin.x, prismaOrigin.z));
        var textMesh = screenText.GetComponent<TextMesh>();

        if (followPrismRod)
        {
            textMesh.text = "Distance: " + Math.Round(distance, 3) + " [m]";
            
            hiddenLaserObj.transform.LookAt(prismaCube.transform);

            transform.rotation = Quaternion.Euler(0, hiddenLaserObj.transform.eulerAngles.y, 0);
            cameraObj.transform.localRotation = Quaternion.Euler(hiddenLaserObj.transform.eulerAngles.x + 90, 0, 0);
        }
        else
        {
            // Do raycasting
            RaycastHit hitInfo;
            if (Physics.Raycast(laserObj.transform.position, laserObj.transform.forward, out hitInfo, 2000))
            {
                textMesh.text = "Distance: " + Math.Round(hitInfo.distance, 3) + " [m]";
            }
            else
            {
                textMesh.text = "Distance: Too far! [m]";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        thumbAxis = context.ReadValue<Vector2>();
        transform.Rotate(0, thumbAxis.x, 0);
        cameraObj.transform.Rotate(-thumbAxis.y, 0, 0);
    }
    
    public void MoveLeft(InputAction.CallbackContext context)
    {
        var leftAxis = context.ReadValue<Vector2>();
        var cameraSoos = cameraObj.GetComponentInChildren<Camera>();
        if (leftAxis.y > 0)
        {
            cameraSoos.fieldOfView -= 0.1f;
        }
        else
        {
            cameraSoos.fieldOfView += + 0.1f;
        }

        if (cameraSoos.fieldOfView > 60)
        {
            cameraSoos.fieldOfView = 60;
        }

        if (cameraSoos.fieldOfView < 2)
        {
            cameraSoos.fieldOfView = 2;
        }
    }

    public void SecondaryPressed(InputAction.CallbackContext context) {
        followPrismRod = !followPrismRod;
    }

    private void OnEnable()
    {
        moveRight.action.Enable();
        moveLeft.action.Enable();
        secondary.action.Enable();
    }

    private void OnDisable()
    {
        moveRight.action.Disable();
        moveLeft.action.Disable();
        secondary.action.Disable();
    }
}