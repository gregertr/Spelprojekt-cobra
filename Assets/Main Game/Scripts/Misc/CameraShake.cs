using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    public float ShakeDuration = 0.3f;          
    public float ShakeAmplitude = 1.5f;        
    public float ShakeFrequency = 2.0f;        

    private float shakeElapsedTime;

    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    [HideInInspector]
    public bool Enable;

    public void Shake()
    {
        Enable = true;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if (Enable)
        {
            Enable = false;
            shakeElapsedTime = ShakeDuration;
        }

        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (shakeElapsedTime > 0)
            {
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }
}
