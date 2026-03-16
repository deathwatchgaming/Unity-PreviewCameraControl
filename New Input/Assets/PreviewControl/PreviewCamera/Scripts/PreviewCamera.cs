/*
 * File: Preview Camera (New Input)
 * Name: PreviewCamera.cs
 * Author: DeathwatchGaming
 * License: MIT 
 */

// using
using UnityEngine;
using UnityEngine.InputSystem;

// namespace PreviewControl
namespace PreviewControl
{
	// Require Component Camera
	[RequireComponent(typeof(Camera))]

	// public class PreviewCamera
	public class PreviewCamera : MonoBehaviour
	{
		// Input Actions
		[Header("Input Actions")]

	        [Tooltip("The input action asset.")]
	        // InputActionAsset _previewCameraControls
	        [SerializeField] private InputActionAsset _previewCameraControls;			
	        
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

		// Input

		// Actions
		private InputAction _moveAction;
		private InputAction _lookAction;
        private InputAction _mouseScrollAction;

        private InputAction _plusSpeedLeftAction;
		private InputAction _plusSpeedRightAction;
		private InputAction _minusSpeedLeftAction;
		private InputAction _minusSpeedRightAction;
		private InputAction _plusLiftAction;
		private InputAction _minusLiftAction;
		private InputAction _cursorLockAction;
		private InputAction _minusFOVAction;
		private InputAction _plusFOVAction;

        // Vector2s
        private Vector2 _moveInput;
		private Vector2 _lookInput;
        private Vector2 _mouseScrollInput;

        // Bools
        private bool _plusSpeedLeftValue;
		private bool _plusSpeedRightValue;
		private bool _minusSpeedLeftValue;
		private bool _minusSpeedRightValue;
		private bool _plusLiftValue;
		private bool _minusLiftValue;
		private bool _cursorLockValue;
		private bool _minusFOVValue;
		private bool _plusFOVValue;

		// Static

		// static PreviewCamera _previewCamera;
		public static PreviewCamera _previewCamera;

		private void Awake()
		{
			_moveAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Move");

			_moveAction.performed += context => _moveInput = context.ReadValue<Vector2>();
			_moveAction.canceled += context => _moveInput = Vector2.zero;

			_lookAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Look");

			_lookAction.performed += context => _lookInput = context.ReadValue<Vector2>();
			_lookAction.canceled += context => _lookInput = Vector2.zero;

            _mouseScrollAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Mouse Scroll");

            _mouseScrollAction.performed += context => _mouseScrollInput = context.ReadValue<Vector2>();
            _mouseScrollAction.canceled += context => _mouseScrollInput = Vector2.zero;
            
			_plusSpeedLeftAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Plus Speed Left");
			_plusSpeedRightAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Plus Speed Right");
			_minusSpeedLeftAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Minus Speed Left");
			_minusSpeedRightAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Minus Speed Right");
			_plusLiftAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Plus Lift");
			_minusLiftAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Minus Lift");
			_cursorLockAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Cursor Lock");
			_minusFOVAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Minus FOV");
			_plusFOVAction = _previewCameraControls.FindActionMap("Preview Camera").FindAction("Plus FOV");
        }

		private void OnEnable()
		{
			_moveAction.Enable();
			_lookAction.Enable();
			_mouseScrollAction.Enable();

            _plusSpeedLeftAction.Enable();
			_plusSpeedRightAction.Enable();
			_minusSpeedLeftAction.Enable();
			_minusSpeedRightAction.Enable();
			_plusLiftAction.Enable();
			_minusLiftAction.Enable();
			_cursorLockAction.Enable();
			_minusFOVAction.Enable();
			_plusFOVAction.Enable();
		}

		private void OnDisable()
		{
			_moveAction.Disable();
			_lookAction.Disable();
			_mouseScrollAction.Disable();

            _plusSpeedLeftAction.Disable();
			_plusSpeedRightAction.Disable();
			_minusSpeedLeftAction.Disable();
			_minusSpeedRightAction.Disable();
			_plusLiftAction.Disable();
			_minusLiftAction.Disable();
			_cursorLockAction.Disable();
			_minusFOVAction.Disable();
			_plusFOVAction.Disable();
		}

		// Start is called before the first frame update

		// private void Start
		private void Start()
		{
			// _previewCamera is this
			_previewCamera = this;

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
			_plusSpeedLeftValue = _plusSpeedLeftAction.IsPressed();
			_plusSpeedRightValue = _plusSpeedRightAction.IsPressed();
			_minusSpeedLeftValue = _minusSpeedLeftAction.IsPressed();
			_minusSpeedRightValue = _minusSpeedRightAction.IsPressed();
			_plusLiftValue = _plusLiftAction.IsPressed();
			_minusLiftValue = _minusLiftAction.IsPressed();
			_cursorLockValue = _cursorLockAction.WasPressedThisFrame();
			_minusFOVValue = _minusFOVAction.WasPressedThisFrame();
            _plusFOVValue = _plusFOVAction.WasPressedThisFrame();

			// _horizontalRotation
			_horizontalRotation += _lookInput.x * _mouseSensitivity * Time.deltaTime;

			// _verticalRotation
			_verticalRotation += _lookInput.y * _mouseSensitivity * Time.deltaTime;

			// _verticalRotation is Mathf Clamp _verticalRotation, -90, 90
			_verticalRotation = Mathf.Clamp(_verticalRotation, -90, 90);

			// transform localRotation
			transform.localRotation = Quaternion.AngleAxis(_horizontalRotation, Vector3.up);

			// transform localRotation
			transform.localRotation *= Quaternion.AngleAxis(_verticalRotation, Vector3.left);

			// if Input GetKey _plusSpeedLeftKey or Input GetKey _plusSpeedRightKey
			if (_plusSpeedLeftValue || _plusSpeedRightValue)
			{
				// transform position
				transform.position += transform.forward * (_defaultMoveSpeed * _plusSpeedMultiplier) * _moveInput.y * Time.deltaTime;

				// transform position
				transform.position += transform.right * (_defaultMoveSpeed * _plusSpeedMultiplier) * _moveInput.x * Time.deltaTime;

			} // close if Input GetKey _plusSpeedLeftKey or Input GetKey _plusSpeedRightKey 

			// else if Input GetKey _minusSpeedLeftKey) or Input GetKey _minusSpeedRightKey
			else if (_minusSpeedLeftValue || _minusSpeedRightValue)
			{
				// transform position
				transform.position += transform.forward * (_defaultMoveSpeed * _minusSpeedMultiplier) * _moveInput.y * Time.deltaTime;

				// transform position
				transform.position += transform.right * (_defaultMoveSpeed * _minusSpeedMultiplier) * _moveInput.x * Time.deltaTime;

			} // close else if Input GetKey _minusSpeedLeftKey) or Input GetKey _minusSpeedRightKey
			
			// else
			else
			{
				// transform position
				transform.position += transform.forward * _defaultMoveSpeed * _moveInput.y * Time.deltaTime;

				// transform position
				transform.position += transform.right * _defaultMoveSpeed * _moveInput.x * Time.deltaTime;

			} // close else

			// if Input GetKey _plusLiftKey
			if (_plusLiftValue) 
			{
				// transform position 
				transform.position += transform.up * _cameraLiftSpeed * Time.deltaTime;

			} // close if Input GetKey _plusLiftKey

			// if Input GetKey _minusLiftKey
			if (_minusLiftValue) 
			{
				// transform position 
				transform.position -= transform.up * _cameraLiftSpeed * Time.deltaTime;

			} // close if Input GetKey _minusLiftKey

			// if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode Locked
			if (_cursorLockValue && Cursor.lockState == CursorLockMode.Locked)
			{
				// Cursor lockState is CursorLockMode None
				Cursor.lockState = CursorLockMode.None;

				// Cursor visible is true
				Cursor.visible = true;

			} // close if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode Locked

			// else if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode None	            
			else if (_cursorLockValue && Cursor.lockState == CursorLockMode.None)
			{
				// Cursor lockState	is CursorLockMode Locked	
				Cursor.lockState = CursorLockMode.Locked;

				// Cursor visible is false
				Cursor.visible = false;

			} // close else if Input GetKeyDown _cursorLockKey and Cursor lockState equals CursorLockMode None

			// currentFieldOfView is _cameraFOV
			currentFieldOfView = _cameraFOV;

			// mouseScroll is Input GetAxis _mouseScrollWheelInput
			mouseScroll = _mouseScrollInput.y;

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
			if (_minusFOVValue)
			{
				// _cameraFOV is minus 1 currentFieldOfView
				_cameraFOV = --currentFieldOfView;

			} // close if Input GetKey _minusFOVKey

			// else if Input GetKey _plusFOVKey
			else if (_plusFOVValue)
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
