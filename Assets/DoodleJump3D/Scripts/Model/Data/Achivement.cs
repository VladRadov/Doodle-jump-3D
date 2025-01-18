using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "ScriptableObject/Achievement")]
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
    public string Description => _description;
    public int CurrentCountSuccess => _currentCountSuccess;
    public int MaxCountSuccess => _maxCountSuccess;
}
