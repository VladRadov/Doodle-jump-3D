using UnityEngine;
using UnityEngine.UI;

public class AchievementsView : MonoBehaviour
{
    [SerializeField] private Button _close;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _close.onClick.AddListener(() => { SetActive(false); });
    }
}
