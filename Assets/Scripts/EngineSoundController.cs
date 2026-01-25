using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
    [Header("Скорость изменения звука")]
    [SerializeField] float soundRate = 1;

    [Header("Максимальная и минимальная тональность звука")]
    [SerializeField] float minimalPitch = 0.2f;
    [SerializeField] float maxPitch = 1f;

    [Header("Максимальная и минимальная громкость звука")]
    [SerializeField] float minimalVolume = 0.3f;
    [SerializeField] float maxVolume = 0.7f;

    float currentPitch;
    float currentVolume;
    AudioSource engineAudio;
    float verticalInput;

    private void Start()
    {
        engineAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput != 0)
        {
            currentPitch = Mathf.Lerp(currentPitch, maxPitch * Mathf.Abs(verticalInput), Time.deltaTime * soundRate);
            currentVolume = Mathf.Lerp(currentVolume, maxVolume * Mathf.Abs(verticalInput), Time.deltaTime * soundRate);
        }
        else if (verticalInput == 0)
        {
            currentPitch = Mathf.Lerp(currentPitch, minimalPitch, Time.deltaTime * soundRate);
            currentVolume = Mathf.Lerp(currentVolume, minimalVolume, Time.deltaTime * soundRate);
        }

        engineAudio.pitch = currentPitch;
        engineAudio.volume = currentVolume;
    }
}