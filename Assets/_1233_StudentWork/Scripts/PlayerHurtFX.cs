using UnityEngine;

public class PlayerHurtFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void HurtParticles()
    {
        if (_particleSystem != null)
            Instantiate(_particleSystem, transform.position, Quaternion.identity);
    }
}
