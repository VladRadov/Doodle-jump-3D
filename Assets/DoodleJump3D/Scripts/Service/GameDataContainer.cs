using UnityEngine;

using UniRx;

public class GameDataContainer : DataContainer<GameDataContainer>
{
    [SerializeField] private BaseGameData _gameData;

    public BaseGameData GameData => _gameData;

    private void Awake()
    {
        SetInstanceCommand.Subscribe(_ => { _gameData.CatSceneView(); });

        Initialize(this);
    }
}
