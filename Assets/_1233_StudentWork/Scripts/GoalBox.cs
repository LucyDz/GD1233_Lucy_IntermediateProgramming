using UnityEngine;

public class GoalBox : MonoBehaviour
{
    [SerializeField] private AudioSource _goalSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _goalSound.Play();
            GameMgr.Instance.NextLevel();
        }
    }
}
