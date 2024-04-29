using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carCameraLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensitivity = 100f;
    Transform carCam;
    float xRotation;
    float yRotation;

    [SerializeField] float upClamp;
    [SerializeField] float downClamp;
    [SerializeField] float leftClamp;
    [SerializeField] float rightClamp;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        carCam = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -downClamp, upClamp);
        yRotation -= mouseX;
        yRotation = Mathf.Clamp(yRotation, -rightClamp, leftClamp);
        transform.localRotation = Quaternion.Euler(xRotation,-yRotation,0f);
        carCam.Rotate(Vector3.up * mouseX);
        carCam.Rotate(Vector3.left * mouseY);
    }
}
