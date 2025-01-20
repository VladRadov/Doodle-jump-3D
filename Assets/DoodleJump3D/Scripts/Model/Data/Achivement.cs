using UnityEngine;

[CreateAssetMenu(fileName = "Achivement", menuName = "ScriptableObject/Achivement")]
public class Achivement : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _maxCountSuccess;
    [SerializeField] private int _currentCountSuccess;
    [SerializeField] private int _levelAchivement;
    [SerializeField] private bool _isAchivementSuccess;

    public int LevelAchivement => _levelAchivement;
    public bool IsAchivementSuccess => _isAchivementSuccess;
    public virtual string Description => _description;
    public int CurrentCountSuccess => _currentCountSuccess;
    public int MaxCountSuccess => _maxCountSuccess;

    public void IncreaseCountSuccess()
    {
        if(_maxCountSuccess != _currentCountSuccess)
            ++_currentCountSuccess;

        if (_maxCountSuccess == _currentCountSuccess && _isAchivementSuccess == false)
            _isAchivementSuccess = true;
    }
}
