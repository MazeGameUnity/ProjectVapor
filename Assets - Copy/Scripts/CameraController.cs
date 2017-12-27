using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraMoveSpeed = 120.0f;
    public GameObject camera;
    Vector3 followPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject cameraObj;
    public GameObject playerObj;
    public float camDistanceX;
    public float camDistanceY;
    public float camDistanceZ;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;
    
	void Start () {

        Vector3 rotation = transform.localRotation.eulerAngles;
        rotY = rotation.y;
        rotX = rotation.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        float InputX = Input.GetAxis("RightStickHorizontal");
        float InputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = InputX + mouseX;
        finalInputZ = InputZ + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
	}

    void LateUpdate()
    {

        CameraUpdater();
    }

    void CameraUpdater()
    {
        //set target object to follow
        Transform target = camera.transform;

        //move towards the game object

        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
