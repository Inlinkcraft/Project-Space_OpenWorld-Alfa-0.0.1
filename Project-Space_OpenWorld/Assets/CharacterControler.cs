using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{

    public readonly static float MOVEMENT_DEAD_ZONE = 0.01f;

    public readonly static float MAX_CAM_TILT = 90f;
    public readonly static float MIN_CAM_TILT = 0f;
    public readonly static float MOVEMENT_SPEED = 10f;
    public readonly static float ROTATION_SPEED = 10f;
    public readonly static float TILT_SPEED = 1f;
    public readonly static float CAM_DISTANCE_PLAYER = 10f;
    
    private float camTheta = 0f;
    private float camTilt = 0f;

    private GameObject CameraAnchorPoint;
    private GameObject playerCam;

    void Awake()
    {
        // TODO : CHANGE CURSOR SETUP PLACE
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // Object caching
        CameraAnchorPoint = transform.Find("CameraAnchorPoint").gameObject;
        playerCam = Camera.main.transform.gameObject;
    }

    void Update()
    {
        CameraInputManager();
        MovementInputManager();
    }

    void CameraInputManager()
    {
        // Take input from the user
        float mx = Input.GetAxis("mouse_horizontal");
        float my = Input.GetAxis("mouse_vertical");

        // Caculate new rotation value
        camTheta += mx * ROTATION_SPEED * Time.deltaTime;
        camTilt += my * ROTATION_SPEED * Time.deltaTime;
        camTilt = Mathf.Clamp(camTilt, MIN_CAM_TILT, MAX_CAM_TILT);

        // Set cam to values
        CameraAnchorPoint.transform.rotation = Quaternion.Euler(camTilt,camTheta,0f);

    }

    void MovementInputManager()
    {
        // Get Inputs
        float x = Input.GetAxis("horizontal");
        float z = Input.GetAxis("vertical");

        // Move
        // Forward
        if (z > MOVEMENT_DEAD_ZONE)
            transform.position += new Vector3(CameraAnchorPoint.transform.forward.x * MOVEMENT_SPEED * Time.deltaTime, 0f, CameraAnchorPoint.transform.forward.z * MOVEMENT_SPEED * Time.deltaTime);
        // Backward
        if (z < -MOVEMENT_DEAD_ZONE)
            transform.position -= new Vector3(CameraAnchorPoint.transform.forward.x * MOVEMENT_SPEED * Time.deltaTime, 0f, CameraAnchorPoint.transform.forward.z * MOVEMENT_SPEED * Time.deltaTime);
        // Right
        if (x > MOVEMENT_DEAD_ZONE)
            transform.position += new Vector3(CameraAnchorPoint.transform.right.x * MOVEMENT_SPEED * Time.deltaTime, 0f, CameraAnchorPoint.transform.right.z * MOVEMENT_SPEED * Time.deltaTime); 
        // Left
        if (x < -MOVEMENT_DEAD_ZONE)
            transform.position -= new Vector3(CameraAnchorPoint.transform.right.x * MOVEMENT_SPEED * Time.deltaTime, 0f, CameraAnchorPoint.transform.right.z * MOVEMENT_SPEED * Time.deltaTime);
    }

}
