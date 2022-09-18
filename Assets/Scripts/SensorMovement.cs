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
    public InputActionReference move = null;
    public Vector2 thumbAxis;

    public GameObject screenText = null;

    private void Awake()
    {
        move.action.performed += Move;
    }

    private void FixedUpdate()
    {
        var laserOrigin = laserObj.transform.position;
        var prismaOrigin = prismaCube.transform.position;
        var distance = Vector2.Distance(new Vector2(laserOrigin.x, laserOrigin.z),
            new Vector2(prismaOrigin.x, prismaOrigin.z));

        var textMesh = screenText.GetComponent<TextMesh>();
        textMesh.text = "Distance: " + Math.Round(distance, 3);

        if (true)
        {
            Vector3 targetVectorCamera = prismaCube.transform.position - cameraObj.transform.position;
            var rotationCamera = Quaternion.FromToRotation(laserObj.transform.forward, targetVectorCamera).eulerAngles;
            
            Vector3 targetVectorBase = prismaCube.transform.position - transform.position;
            var rotationBase = Quaternion.FromToRotation(transform.forward, targetVectorBase).eulerAngles;
            
            transform.Rotate(0, rotationBase.y, 0);
            cameraObj.transform.Rotate(rotationCamera.x, 0, 0);
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