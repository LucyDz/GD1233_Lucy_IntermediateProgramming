using UnityEngine;

public class SetAsPlayer : MonoBehaviour
{
    void Start()
    {
        PlayerMgr.Instance.DebugAssignAsPlayer(gameObject);
    }
}
