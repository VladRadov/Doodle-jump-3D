using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameData", menuName = "ScriptableObject/BaseGameData")]
public class BaseGameData : ScriptableObject
{
    [SerializeField] private bool _isEducation;

    public virtual int CurrentResult { get; set; }
    public virtual int BestResult { get; set; }
    public virtual bool IsEducation => _isEducation;

    public void EndEducation()
        => _isEducation = true;
}
