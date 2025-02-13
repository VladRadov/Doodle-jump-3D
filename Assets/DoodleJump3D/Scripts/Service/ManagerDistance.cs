using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerDistance : BaseManager
{
    private int _currentDistace;

    [SerializeField] private Text _distance;

    public override void Initialize()
    {
        ResetDistance();
    }

    public void IncreasingDistance(int valueDistance)
    {
        _currentDistace = valueDistance;
        UpdateDistance();
    }

    public void SaveResult()
    {
        GameDataContainer.Instance.GameData.CurrentResult = _currentDistace;

        if(GameDataContainer.Instance.GameData.BestResult < _currentDistace)
            GameDataContainer.Instance.GameData.BestResult = _currentDistace;
    }

    private void ResetDistance()
    {
        _currentDistace = 0;
        UpdateDistance();
    }

    private void UpdateDistance()
        => _distance.text = _currentDistace.ToString();
}
