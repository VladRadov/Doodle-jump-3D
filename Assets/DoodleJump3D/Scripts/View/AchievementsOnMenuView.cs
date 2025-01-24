using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AchievementsOnMenuView : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private List<Achievement> _achievements;
    [Header("UI")]
    [SerializeField] private Button _close;
    [SerializeField] private AchievementItemView _prefabAchievementItemView;
    [SerializeField] private Transform _parentListAchievementsView;

    [HideInInspector]
    public UnityEvent ClosingEventHandler;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _close.onClick.AddListener(() => { ClosingEventHandler?.Invoke(); });
        LoadingAchievementsItem();
    }

    private void LoadingAchievementsItem()
    {
        foreach (var achivement in _achievements)
        {
            var achievementItemView = PoolObjects<AchievementItemView>.GetObject(_prefabAchievementItemView, _parentListAchievementsView);
            achievementItemView.Initialize(achivement);
        }
    }

    private void OnValidate()
    {
        if (_parentListAchievementsView == null)
            Debug.LogError("AchievementsOnMenuView не назначен родитель для списка ачивок _parentListAchievementsView!");
    }
}
