using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class GamePanelView : MonoBehaviour
{
    [SerializeField] private Button _shot;

    public ReactiveCommand ShootingCommand = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void Initialize(bool isPlatformPC)
    {
        if (isPlatformPC)
            _shot.gameObject.SetActive(false);
        else
        {
            _shot.gameObject.SetActive(true);
            _shot.onClick.AddListener(() => { ShootingCommand.Execute(); });
        }

    }
}
