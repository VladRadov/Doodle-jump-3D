using UnityEngine;

using UniRx;

public class InvisibleComponent : BaseComponent
{
    [SerializeField] private float _duration;

    public ReactiveCommand HidePlatformCommand;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Doodle"))
        {
            HidePlatformCommand.Execute();
            gameObject.SetActive(false);
        }
    }

    public override void Start()
    {
        base.Start();
        ManagerUniRx.AddObjectDisposable(HidePlatformCommand);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(HidePlatformCommand);
    }
}
