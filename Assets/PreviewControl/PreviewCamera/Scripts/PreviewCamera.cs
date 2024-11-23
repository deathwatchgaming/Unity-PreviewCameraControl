/*
 * File: Preview Camera
 * Name: PreviewCamera.cs
 * Author: DeathwatchGaming
 * License: MIT 
 */

// using
using UnityEngine;

// namespace PreviewControl
namespace PreviewControl
{
	// Require Component Camera
	[RequireComponent(typeof(Camera))]

	// public class PreviewCamera
	public class PreviewCamera : MonoBehaviour
	{
		// Input Customizations
		[Header("Input Customizations")]
	        
			[Tooltip("The vertical movement input string")]
			// string _verticalMoveInput is Vertical
			[SerializeField] private string _verticalMoveInput = "Vertical";	
	        
			[Tooltip("The horizontal movement input string")]
			// string _horizontalMoveInput is Horizontal
			[SerializeField] private string _horizontalMoveInput = "Horizontal";

			[Tooltip("The mouse scrollwheel input string")]
			// string _mouseScrollWheelInput is Mouse ScrollWheel
			[SerializeField] private string _mouseScrollWheelInput = "Mouse ScrollWheel";			
	        
			[Tooltip("The mouse Y input string")]
			// string _mouseYInput is Mouse Y          
			[SerializeField] private string _mouseYInput = "Mouse Y";
	        
			[Tooltip("The mouse X input string")]
			// string _mouseXInput is Mouse X
			[SerializeField] private string _mouseXInput = "Mouse X";
	        
			[Tooltip("The left increase movement speed input keycode key")]
			// KeyCode _plusSpeedLeftKey is KeyCode.LeftShift
			[SerializeField] private KeyCode _plusSpeedLeftKey = KeyCode.LeftShift;

			[Tooltip("The right increase movement speed input keycode key")]
			// KeyCode _plusSpeedRightKey is KeyCode.RightShift
			[SerializeField] private KeyCode _plusSpeedRightKey = KeyCode.RightShift;

			[Tooltip("The left decrease movement speed input keycode key")]
			// KeyCode _minusSpeedLeftKey is KeyCode.LeftControl;
			[SerializeField] private KeyCode _minusSpeedLeftKey = KeyCode.LeftControl;

			[Tooltip("The right decrease movement speed input keycode key")]
			// KeyCode _minusSpeedRightKey is KeyCode.RightControl
			[SerializeField] private KeyCode _minusSpeedRightKey = KeyCode.RightControl;

			[Tooltip("The plus camera lift movement input keycode key")]
			// KeyCode _plusLiftKey is KeyCode.Q
			[SerializeField] private KeyCode _plusLiftKey = KeyCode.Q;

			[Tooltip("The minus camera lift movement input keycode key")]
			// KeyCode _minusLiftKey is KeyCode.E
			[SerializeField] private KeyCode _minusLiftKey = KeyCode.E;

			[Tooltip("The cursor lock input keycode key")]
			// KeyCode _cursorLockKey is KeyCode.End
			[SerializeField] private KeyCode _cursorLockKey = KeyCode.End;

			[Tooltip("The camera minus field of view input keycode key")]
			// KeyCode _minusFOVKey is KeyCode.Z
			[SerializeField] private KeyCode _minusFOVKey = KeyCode.Z;

			[Tooltip("The camera plus field of view input keycode key")]
			// KeyCode _plusFOVKey is KeyCode.X
			[SerializeField] private KeyCode _plusFOVKey = KeyCode.X;

		// Amounts
		[Header("Amounts")]

			[Tooltip("The camera rotation mouse sensitivity amount")]
			// float _mouseSensitivity is 90
			[SerializeField] private float _mouseSensitivity = 90;

			[Tooltip("The camera default movement speed amount")]
			// float _defaultMoveSpeed is 10
			[SerializeField] private float _defaultMoveSpeed = 10;

			[Tooltip("The camera increased movement speed multiplier amount")]
			// float _plusSpeedMultiplier is 3
			[SerializeField] private float _plusSpeedMultiplier = 3;

			[Tooltip("The camera decreased movement speed multiplier amount")]
			// float _minusSpeedMultiplier is 0.25
			[SerializeField] private float _minusSpeedMultiplier = 0.25f;

			[Tooltip("The camera lift speed amount")]
			// float _cameraLiftSpeed is 4
			[SerializeField] private float _cameraLiftSpeed = 4;

			[Tooltip("The desired field of view amount")]
			// float _cameraFOV is 60
			[SerializeField] private float _cameraFOV = 60f;

			[Tooltip("The zoom ratio amount")]
			// float _zoomRatio is 0.5
			[SerializeField] private float _zoomRatio = 0.5f;

			[Tooltip("The minimum field of view amount")]
			// float _minimumFOV is 1
			[SerializeField,Range(float.Epsilon, 179f)] private float _minimumFOV = 1f;
			
			[Tooltip("The maximum field of view amount")]
			// float _maximumFOV is 118	
			[SerializeField,Range(float.Epsilon, 179f)] private float _maximumFOV = 118f;

		// Enabled State
		[Header("Enabled State")]

			[Tooltip("The Preview Camera enabled state")]
			// bool PreviewCameraEnabled is true
			public bool PreviewCameraEnabled = true;

		// Private

		// float _horizontalRotation is 0.0
		private float _horizontalRotation = 0.0f;

		// float _verticalRotation is 0.0
		private float _verticalRotation = 0.0f;

		// float currentFieldOfView is 0
		private float currentFieldOfView = 0f;

		// float _minFieldOfView is 0
		private float _minFieldOfView = 0f;

		// float _maxFieldOfView is 0
		private float _maxFieldOfView = 0f;

		// float mouseScroll is 0
		private float mouseScroll = 0f;

		// Start is called before the first frame update

		// private void Start
		private void Start()
		{
			// Cursor lockState is CursorLockMode Locked
			Cursor.lockState = CursorLockMode.Locked;

			// Cursor visible is false
			Cursor.visible = false;

		} // close private void Start

		// Update is called every frame

		// private void Update
		private void Update()
		{
			// if PreviewCameraEnabled equals true
			if (PreviewCameraEnabled == true)
			{
				// GetComponent PreviewCamera is enabled
				GetComponent<PreviewCamera>().enabled = true;

				//Debug.Log("The Preview Camera is enabled");

				// Update Preview Camera
				UpdatePreviewCamera();

			} // close if PreviewCameraEnabled equals true

			// else if PreviewCameraEnabled equals false
			else if (PreviewCameraEnabled == false)
			{
				//Debug.Log("The Preview Camera is disabled");

				// GetComponent PreviewCamera is not enabled
				GetComponent<PreviewCamera>().enabled = false;

			} // close else if PreviewCameraEnabled equals false

		}  // close private void Update

		// UpdatePreviewCamera is called in Update

		// private void UpdatePreviewCamera
		private void UpdatePreviewCamera()
		{
			// _horizontalRotation
			_horizontalRotation += Input.GetAxis(_mouseXInput) * _mouseSensitivity * Time.deltaTime;

			// _verticalRotation
			_verticalRotation += Input.GetAxis(_mouseYInput) * _mouseSensitivity * Time.deltaTime;

			// _verticalRotation is Mathf Clamp _verticalRotation, -90, 90
			_verticalRotation = Mathf.Clamp(_verticalRotation, -90, 90);

			// transform localRotation
			transform.localRotation = Quaternion.AngleAxis(_horizontalRotation, Vector3.up);

			// transform localRotation
			transform.localRotation *= Quaternion.AngleAxis(_verticalRotation, Vector3.left);

			// if Input GetKey _plusSpeedLeftKey or Input GetKey _plusSpeedRightKey
			if (Input.GetKey(_plusSpeedLeftKey) || Input.GetKey(_plusSpeedRightKey))
			{
				// transform position
				transform.position += transform.forward * (_defaultMoveSpeed * _plusSpeedMultiplier) * Input.GetAxis(_verticalMoveInput) * Time.deltaTime;

				// transform position
				transform.position += transform.right * (_defaultMoveSpeed * _plusSpeedMultiplier) * Input.GetAxis(_horizontalMoveInput) * Time.deltaTime;

			} // close if Input GetKey _plusSpeedLeftKey or Input GetKey _plusSpeedRightKey 

			// else if Input GetKey _minusSpeedLeftKey) or Input GetKey _minusSpeedRightKey
			else if (Input.GetKey(_minusSpeedLeftKey) || Input.GetKey(_minusSpeedRightKey))
			{
				// transform position
				transform.position += transform.forward * (_defaultMoveSpeed * _minusSpeedMultiplier) * Input.GetAxis(_verticalMoveInput) * Time.deltaTime;

				// transform position
				transform.position += transform.right * (_defaultMoveSpeed * _minusSpeedMultiplier) * Input.GetAxis(_horizontalMoveInput) * Time.deltaTime;

			} // close else if Input GetKey _minusSpeedLeftKey) or Input GetKey _minusSpeedRightKey
			
			// else
			else
			{
				// transform position
				transform.position += transform.forward * _defaultMoveSpeed * Input.GetAxis(_verticalMoveInput) * Time.deltaTime;

				// transform position
				transform.position += transform.right * _defaultMoveSpeed * Input.GetAxis(_horizontalMoveInput) * Time.deltaTime;

			} // close else

			// if Input GetKey _plusLiftKey
			if (Input.GetKey(_plusLiftKey)) 
			{
				// transform position 
				transform.position += transform.up * _cameraLiftSpeed * Time.deltaTime;

			} // close if Input GetKey _plusLiftKey

			// if Input GetKey _minusLiftKey
			if (Input.GetKey(_minusLiftKey)) 
			{
				// transform position 
				transform.position -= transform.up * _cameraLiftSpeed * Time.deltaTime;

			} // close if Input GetKey _minusLiftKey

			// if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode Locked
			if (Input.GetKeyDown(_cursorLockKey) && Cursor.lockState == CursorLockMode.Locked)
			{
				// Cursor lockState is CursorLockMode None
				Cursor.lockState = CursorLockMode.None;

				// Cursor visible is true
				Cursor.visible = true;

			} // close if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode Locked

			// else if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode None	            
			else if (Input.GetKeyDown(_cursorLockKey) && Cursor.lockState == CursorLockMode.None)
			{
				// Cursor lockState	is CursorLockMode Locked	
				Cursor.lockState = CursorLockMode.Locked;

				// Cursor visible is false
				Cursor.visible = false;

			} // close else if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode None

			// currentFieldOfView is _cameraFOV
			currentFieldOfView = _cameraFOV;

			// mouseScroll is Input GetAxis _mouseScrollWheelInput
			mouseScroll = Input.GetAxis(_mouseScrollWheelInput);

			// if mouseScroll greater than 0
			if (mouseScroll > 0)
			{
				// _cameraFOV is plus 1 currentFieldOfView
				_cameraFOV = ++currentFieldOfView;

			} // close if mouseScroll greater than 0

			// else if mouseScroll less than 0
			else if (mouseScroll < 0)
			{
				// _cameraFOV is minus 1 currentFieldOfView
				_cameraFOV = --currentFieldOfView;

			} // close else if mouseScroll less than 0

			// if Input GetKey _minusFOVKey
			if (Input.GetKey(_minusFOVKey))
			{
				// _cameraFOV is minus 1 currentFieldOfView
				_cameraFOV = --currentFieldOfView;

			} // close if Input GetKey _minusFOVKey

			// else if Input GetKey _plusFOVKey
			else if (Input.GetKey(_plusFOVKey))
			{
				// _cameraFOV is plus 1 currentFieldOfView
				_cameraFOV = ++currentFieldOfView;

			} // close else if Input GetKey _plusFOVKey

			// _minFieldOfView is Mathf Clamp _minimumFOV, float Epsilon, _maximumFOV
			_minFieldOfView = Mathf.Clamp(_minimumFOV, float.Epsilon, _maximumFOV);

			// _maxFieldOfView is Mathf Clamp _maximumFOV, _minimumFOV, 179
			_maxFieldOfView = Mathf.Clamp(_maximumFOV, _minimumFOV, 179f);

			// _minimumFOV is _minFieldOfView;
			_minimumFOV = _minFieldOfView;

			// _maximumFOV is _maxFieldOfView
			_maximumFOV = _maxFieldOfView;

			// _cameraFOV is Mathf Clamp currentFieldOfView, _minimumFOV, _maximumFOV
			_cameraFOV = Mathf.Clamp(currentFieldOfView, _minimumFOV, _maximumFOV);

			// GetComponent Camera fieldOfView is _cameraFOV plus _zoomRatio times Time deltaTime
			GetComponent<Camera>().fieldOfView = _cameraFOV + _zoomRatio * Time.deltaTime;
			
		} // close private void UpdatePreviewCamera

	} // close public class PreviewCamera

} // close namespace PreviewControl
