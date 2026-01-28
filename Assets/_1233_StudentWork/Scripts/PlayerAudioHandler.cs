using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{

    [SerializeField] private AudioSource _footstepSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void PlayFootstep()
    {
        _footstepSource?.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
