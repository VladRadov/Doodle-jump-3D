using UnityEngine;
using UnityEngine.UI;

using UniRx;

public class ClueView : MonoBehaviour
{
    [SerializeField] private Button _ok;

    public ReactiveCommand OnOkClick = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _ok.onClick.AddListener(() => { OnOkClick.Execute(); });
        ManagerUniRx.AddObjectDisposable(OnOkClick);
    }

    private void OnDisable()
    {
        ManagerUniRx.Dispose(OnOkClick);
    }
}
