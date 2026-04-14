using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{

    [SerializeField] private AudioSource _footstepSource;
    [SerializeField] private AudioSource _jumpSource;
    [SerializeField] private AudioSource _jumplandSource;
    [SerializeField] private AudioSource _shootSource;
    [SerializeField] private AudioSource _healSource;
    [SerializeField] private AudioSource _hurtSource;
    [SerializeField] private AudioSource _deathSource;
    [SerializeField] private AudioSource _throwSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    #region called from animation
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
    public void PlayShoot()
    {
        _shootSource?.Play();
    }
    public void PlayHurt()
    {
        _hurtSource?.Play();
    }
    #endregion
    public void PlayHeal()
    {
        _healSource?.Play();
    }
    public void PlayDeath()
    {
        _deathSource?.Play();
    }
    public void PlayThrow()
    {
        _throwSource?.Play();
    }
}

    
