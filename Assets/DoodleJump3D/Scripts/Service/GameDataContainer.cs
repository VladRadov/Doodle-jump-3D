using UnityEngine;

public class GameDataContainer : DataContainer<GameDataContainer>
{
    [SerializeField] private GameData _gameData;

    public GameData GameData => _gameData;

    private void Awake()
    {
        Initialize(this);
    }
}
