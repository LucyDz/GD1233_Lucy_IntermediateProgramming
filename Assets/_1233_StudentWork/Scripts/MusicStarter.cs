using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [SerializeField] private AudioMgr.MusicTypes _Song;

    private void OnEnable()
    {
        AudioMgr.Instance.PlayMusic(_Song, 1);
    }
}
