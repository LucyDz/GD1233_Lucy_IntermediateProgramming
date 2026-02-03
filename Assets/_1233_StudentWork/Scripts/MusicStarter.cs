using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [SerializeField] private AudioMgr.MusicTypes _Song;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        AudioMgr.Instance.PlayMusic(_Song, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
