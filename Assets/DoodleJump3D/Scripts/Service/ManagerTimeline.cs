using UnityEngine;
using UnityEngine.Playables;

public class ManagerTimeline : BaseManager
{
    [SerializeField] private PlayableDirector _playableDirector;

    public override void Initialize()
    {
        _playableDirector.enabled = false;
    }

    public void SetActiveTimelinePlayableDirector(bool value)
        => _playableDirector.enabled = value;
}
