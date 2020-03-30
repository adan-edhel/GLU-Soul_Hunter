﻿using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    [SerializeField] float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Awake()
    {
        Instance = this;
        VirtualCamera =  GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        if (VirtualCamera != null)
        {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void UpdateConfiner(PolygonCollider2D collider)
    {
        VirtualCamera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = collider;
    }

    /// <summary>
    /// Shakes the camera with the following parameters: Duration, Amplitude, Frequency. -- Duration always needs to be given a value, amplitude and frequency will use default value when left at 0. Defaults: Amp(1.2f), Freq(2.0f).
    /// </summary>
    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
        ShakeElapsedTime = duration / 10;

        // Set Cinemachine Camera Noise parameters
        if (amplitude <= 0)
        {
            virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
        }
        else
        {
            virtualCameraNoise.m_AmplitudeGain = amplitude;
        }
        if (frequency <= 0)
        {
            virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
        }
        else
        {
            virtualCameraNoise.m_FrequencyGain = frequency;
        }
    }

    private void Update()
    {
        // If the Cinemachine components are not set, avoid update
        if (VirtualCamera != null || virtualCameraNoise != null)
        {
            // Set Cinemachine Camera Noise parameters
            if (ShakeElapsedTime > 0)
            {
                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
}
