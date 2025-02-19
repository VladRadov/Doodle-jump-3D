using UnityEngine;

public class ManagerStars : BaseManager
{
    private int _indexPlatformForSpawn;

    [SerializeField] private StarView _starViewPrefab;

    public override void Initialize()
    {

    }

    public void GetRandomIndexPlatformForSpawnRocket(int countStartPlatform)
        => _indexPlatformForSpawn = Random.Range(0, countStartPlatform);

    public void SpawStar(Transform parent, int indexCurrentPlatform)
    {
        if (indexCurrentPlatform == _indexPlatformForSpawn)
        {
            var starView = PoolObjects<StarView>.GetObject(_starViewPrefab, parent);
            starView.transform.localPosition = new Vector3(0, 5, 0);
        }
    }
}
