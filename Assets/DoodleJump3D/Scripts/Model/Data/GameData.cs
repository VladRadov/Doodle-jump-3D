using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObject/GameData")]
public class GameData : ScriptableObject
{
    public int CurrentResult { get; set; }
    public int BestResult { get; set; }
}
