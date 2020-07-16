using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float smoothing;

    private GameObject player;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPosition;

    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        //Mouse input on camera to rotate player.
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //Scaling down the input variables from the mouse movement.
        inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));
        //Camera smoothing.
        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPosition += smoothedVelocity;
        //Vertical Looking
        transform.localRotation = Quaternion.AngleAxis(-currentLookingPosition.y, Vector3.right);
        //Horizontal Looking
        player.transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up);
        currentLookingPosition.y = Mathf.Clamp(currentLookingPosition.y, -80f, 80f);
    }
    
}
