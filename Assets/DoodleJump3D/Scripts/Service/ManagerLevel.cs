public class ManagerLevel : BaseManager
{
    private bool _isPause;

    public bool IsPause => _isPause;

    public override void Initialize()
    {
        _isPause = true;
    }

    public void SetActivePause(bool value)
        => _isPause = value;
}
