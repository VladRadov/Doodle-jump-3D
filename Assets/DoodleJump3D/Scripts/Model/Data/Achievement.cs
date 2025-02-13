using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "Achivement", menuName = "ScriptableObject/Achivement")]
public class Achievement : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _nameEnglish;
    [SerializeField] private string _description;
    [SerializeField] private string _descriptionEnglish;
    [SerializeField] private int _maxCountSuccess;
    [SerializeField] private int _currentCountSuccess;
    [SerializeField] private int _levelAchievement;
    [SerializeField] private bool _isAchievementSuccess;
    [SerializeField] private Sprite _icon;

    public int LevelAchievement => _levelAchievement;
    public bool IsAchievementSuccess => _isAchievementSuccess;
    public virtual string Description { get { return YandexGame.savesData.language == "ru" ? _description : _descriptionEnglish; } }
    public virtual string Name { get { return YandexGame.savesData.language == "ru" ? _name : _nameEnglish; } }
    public int CurrentCountSuccess => _currentCountSuccess;
    public int MaxCountSuccess => _maxCountSuccess;
    public Sprite Icon => _icon;

    public void IncreaseCountSuccess()
    {
        if(_maxCountSuccess != _currentCountSuccess)
            ++_currentCountSuccess;

        CheckSuccessAchievement();
    }

    public void IncreaseCountSuccess(int value)
    {
        if (_maxCountSuccess != _currentCountSuccess)
            _currentCountSuccess = value;

        CheckSuccessAchievement();
    }

    public void CheckSuccessAchievement()
    {
        if (_maxCountSuccess == _currentCountSuccess && _isAchievementSuccess == false)
            _isAchievementSuccess = true;
    }
}
