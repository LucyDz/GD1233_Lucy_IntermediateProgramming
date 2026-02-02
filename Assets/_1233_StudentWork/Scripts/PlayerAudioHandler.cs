using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{

    [SerializeField] private AudioSource _footstepSource;
    [SerializeField] private AudioSource _jumpSource;
    [SerializeField] private AudioSource _jumplandSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void PlayFootstep()
    {
        _footstepSource?.Play();
    }
    public void PlayJumpUp()
    {
        _jumpSource?.Play();
    }
    public void PlayJumpLand()
    {
        _jumplandSource?.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
