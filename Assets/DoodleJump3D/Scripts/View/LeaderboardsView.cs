using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeaderboardsView : MonoBehaviour
{
    [SerializeField] private Button _close;

    [HideInInspector]
    public UnityEvent ClosingEventHandler;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _close.onClick.AddListener(() => { ClosingEventHandler?.Invoke(); });
    }
}
