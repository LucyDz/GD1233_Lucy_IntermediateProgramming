using UnityEngine;

public class GoalBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMgr.Instance.NextLevel();
        }
    }
}
