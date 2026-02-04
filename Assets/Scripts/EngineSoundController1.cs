using UnityEngine;

public class EngineSoundAlwaysOn : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}