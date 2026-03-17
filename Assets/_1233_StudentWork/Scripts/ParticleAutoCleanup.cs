using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParticleAutoCleanup : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        if (_particleSystem == null)
            _particleSystem = GetComponent<ParticleSystem>();
    }

  
    private void OnEnable()
    {
        StartCoroutine(PlayAndCleanup());   
    }

    private IEnumerator PlayAndCleanup()
    {
        _particleSystem.Play();

        // wait until particle system fully finishes
        yield return new WaitUntil(() => !_particleSystem.IsAlive(true));

        Destroy(gameObject);
    }
}
