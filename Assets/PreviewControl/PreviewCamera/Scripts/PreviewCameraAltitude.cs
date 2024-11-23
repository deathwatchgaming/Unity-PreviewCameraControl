/*
 * File: Preview Camera Altitude
 * Name: PreviewCameraAltitude.cs
 * Author: DeathwatchGaming
 * License: MIT
 */

// using
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

// namespace PreviewControl
namespace PreviewControl
{
    // public enum AltitudeType
    public enum CameraAltitudeType
    {
        ft,
        m 

    } // close public enum AltitudeType

    // public class PreviewCameraAltitude
    public class PreviewCameraAltitude : MonoBehaviour
    {

        // Game Object
        [Header("Game Object")]

            [Tooltip("The Preview Camera Altitude Parent Game Object")]
            // GameObject _cameraAltitudeParent
            [SerializeField] private GameObject _cameraAltitudeParent;

        // Raw Image
        [Header("Raw Image")]

            [Tooltip("The Preview Camera HUD Interface Altitude Text background raw image")]
            // RawImage _altitudeBackground
            [SerializeField] private RawImage _altitudeBackground;

        // HUD Text
        [Header("Text")]

            [Tooltip("The Preview Camera HUD Interface Altitude TextMeshPro Text")]
            // TextMeshProUGUI _cameraAltitude
            [SerializeField] private TextMeshProUGUI _cameraAltitude;

        // Altitude Type
        [Header("Type")]

            [Tooltip("The Altitude Measurement Unit")]
            // CameraAltitudeType _cameraAltitudeType   
            [SerializeField] private CameraAltitudeType _cameraAltitudeType;

        // Enabled State
        [Header("Enabled State")]

            [Tooltip("The Preview Camera Altitude HUD enabled state")]
            // bool cameraAltitudeEnabled is true
            public bool cameraAltitudeEnabled = true;

        // Public Static

        // PreviewCameraAltitude _previewCameraAltitude
        public static PreviewCameraAltitude _previewCameraAltitude;

        // Start is called before the first frame update 

        // private void Start
        private void Start()
        {
            // _previewCameraAltitude is this
            _previewCameraAltitude = this;

            // _cameraAltitude fontStyle is FontStyles SmallCaps
            _cameraAltitude.fontStyle = FontStyles.SmallCaps;

            // _cameraAltitude enableAutoSizing is true
            _cameraAltitude.enableAutoSizing = true;

        } // close private void Start 


        // Update is called every frame        

        // private void Update
        private void Update()
        {
            // if cameraAltitudeEnabled is true
            if (cameraAltitudeEnabled == true)
            {
                // cameraAltitudeParent gameObject SetActive is true
                _cameraAltitudeParent.gameObject.SetActive(true);

                // _cameraAltitude gameObject SetActive is true
                _cameraAltitude.gameObject.SetActive(true);

                // _altitudeBackground gameObject SetActive is true
                _altitudeBackground.gameObject.SetActive(true);

                // Get Component PreviewCameraAltitude enabled is true
                GetComponent<PreviewCameraAltitude>().enabled = true;

                // Debug Log Preview Camera Altitude is enabled
                //Debug.Log("The Preview Camera Altitude is enabled");

                // Update HUD
                UpdateHUD();

            } // close if cameraAltitudeEnabled is true

            // else if cameraAltitudeEnabled is false
            else if (cameraAltitudeEnabled == false)
            {
                // Debug Log Preview Camera Altitude is disabled
                //Debug.Log("The Preview Camera Altitude is disabled");

                // cameraAltitudeParent gameObject SetActive is false
                _cameraAltitudeParent.gameObject.SetActive(false);

                // _cameraAltitude gameObject SetActive is false
                _cameraAltitude.gameObject.SetActive(false);

                // _altitudeBackground gameObject SetActive is false
                _altitudeBackground.gameObject.SetActive(false);

                // Get Component PreviewCameraAltitude enabled is false
                GetComponent<PreviewCameraAltitude>().enabled = false;

            } // close else if cameraAltitudeEnabled is false

        } // close private void Update

        // UpdateHUD
        private void UpdateHUD()
        {
            // if _cameraaltitudeType is  CameraAltitudeType ft
            if (_cameraAltitudeType == CameraAltitudeType.ft)
            {
                // _cameraAltitude text Altitude: ft
                _cameraAltitude.text = "Altitude: " + (transform.position.y / 0.3048f).ToString("F0") + " ft";

            } // close if _cameraaltitudeType is  CameraAltitudeType ft

            // else if _cameraAltitudeType is  CameraAltitudeType m
            else if (_cameraAltitudeType == CameraAltitudeType.m)
            {
                // _cameraAltitude text Altitude: m
                _cameraAltitude.text  = "Altitude: " + transform.position.y.ToString("F0") + " m";

            } // close else if _cameraAltitudeType is  CameraAltitudeType m

        }  // close UpdateHUD

    } // close public class PreviewCameraAltitude

} // close namespace PreviewControl
