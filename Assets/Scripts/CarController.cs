using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle;
	public float motorForce;
    public float maxAcceleration;
    public float brakeForce;
    public float max_speed;
	public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxisRaw("Vertical");
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{   
        float speed = GetComponent<Rigidbody>().velocity.sqrMagnitude;

        if(speed < max_speed) 
        {
            float torque = m_verticalInput * motorForce;
            frontDriverW.motorTorque = Mathf.Clamp(torque, -maxAcceleration, maxAcceleration);
            frontPassengerW.motorTorque = Mathf.Clamp(torque, -maxAcceleration, maxAcceleration);
        }
        else
        {
            frontDriverW.motorTorque = 0;
            frontPassengerW.motorTorque = 0;
        }
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

    private void ApplyBrake()
    {
		float speed = GetComponent<Rigidbody>().velocity.sqrMagnitude;

        float brakeforce;
        if(Input.GetKey(KeyCode.S) && speed > 0)
        {
            brakeforce = brakeForce;
        }

        else
        {
            brakeforce = 0; 
        }

        frontDriverW.brakeTorque = brakeforce;
        frontPassengerW.brakeTorque = brakeforce;
        rearPassengerW.brakeTorque = brakeforce;
        rearDriverW.brakeTorque = brakeforce;
    }

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
        ApplyBrake();

	}

	
}
