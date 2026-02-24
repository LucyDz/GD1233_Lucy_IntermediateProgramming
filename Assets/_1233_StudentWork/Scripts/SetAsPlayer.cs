using UnityEngine;

public class SetAsPlayer : MonoBehaviour
{
    void Awake()
    {
        PlayerMgr.Instance.DebugAssignAsPlayer(gameObject);
    }
}
