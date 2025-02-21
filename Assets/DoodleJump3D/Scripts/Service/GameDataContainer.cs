using UnityEngine;

using UniRx;

public class GameDataContainer : DataContainer<GameDataContainer>
{
    [SerializeField] private BaseGameData _gameData;

    public BaseGameData GameData => _gameData;

    public override void Initialize()
    {
        SetInstanceCommand.Subscribe(_ => { _gameData.CatSceneView(); });

        Initialize(this);
    }
}
