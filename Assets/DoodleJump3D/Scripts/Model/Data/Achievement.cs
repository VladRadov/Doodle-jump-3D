using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "ScriptableObject/Achievement")]
public class Achievement : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _maxCountSuccess;
    [SerializeField] private int _currentCountSuccess;
    [SerializeField] private int _levelAchievement;
    [SerializeField] private bool _isAchievementSuccess;
}
