using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AchievementsView : MonoBehaviour
{
    [SerializeField] private Button _close;

    public UnityEvent ClosingEventHandler;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _close.onClick.AddListener(() => { ClosingEventHandler?.Invoke(); });
    }
}
