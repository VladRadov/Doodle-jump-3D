using UnityEngine;

public class GameDataContainer : DataContainer<GameDataContainer>
{
    [SerializeField] private BaseGameData _gameData;

    public BaseGameData GameData => _gameData;

    private void Awake()
    {
        Initialize(this);
    }
}
