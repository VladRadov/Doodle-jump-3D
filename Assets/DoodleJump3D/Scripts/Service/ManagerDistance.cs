using UnityEngine;

using TMPro;
using UniRx;

public class ManagerDistance : BaseManager
{
    private int _currentDistace;

    [SerializeField] private TextMeshProUGUI _distance;

    public ReactiveCommand<int> ChangedBestDistance = new();

    public override void Initialize()
    {
        ResetDistance();
        ManagerUniRx.AddObjectDisposable(ChangedBestDistance);
    }

    public void IncreasingDistance(int valueDistance)
    {
        _currentDistace = valueDistance;
        UpdateDistance();
    }

    public void SaveResult()
    {
        GameDataContainer.Instance.GameData.CurrentResult = _currentDistace;

        if (GameDataContainer.Instance.GameData.BestResult < _currentDistace)
        {
            GameDataContainer.Instance.GameData.BestResult = _currentDistace;
            ChangedBestDistance.Execute(_currentDistace);
        }
    }

    private void ResetDistance()
    {
        _currentDistace = 0;
        UpdateDistance();
    }

    private void UpdateDistance()
        => _distance.text = _currentDistace.ToString();

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(ChangedBestDistance);
    }
}
