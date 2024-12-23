using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameData", menuName = "ScriptableObject/BaseGameData")]
public class BaseGameData : ScriptableObject
{
    public virtual int CurrentResult { get; set; }
    public virtual int BestResult { get; set; }
}
