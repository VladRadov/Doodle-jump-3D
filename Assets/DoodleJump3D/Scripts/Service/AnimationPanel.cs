using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class AnimationPanel
{
    private Vector2 EndPositionActive = new Vector2(502, -232);
    private Vector2 EndPositionHide = new Vector2(502, 500);
    private readonly float _speed;

    public AnimationPanel(Vector2 endPositionActive, Vector2 endPositionHide, float speed)
    {
        EndPositionActive = endPositionActive;
        EndPositionHide = endPositionHide;
        _speed = speed;
    }

    public async void MoveDOAchorPos(GameObject gameObjectActive, GameObject gameObjectHide = null)
    {
        gameObjectActive?.SetActive(true);

        if (gameObjectHide != null)
        {
            var transformHide = gameObjectHide.GetComponent<RectTransform>();
            transformHide.DOAnchorPos(EndPositionHide, _speed);
            await UniTask.Delay(500);

            gameObjectHide.SetActive(false);
        }
        if (gameObjectActive != null)
        {
            var transformActive = gameObjectActive.GetComponent<RectTransform>();
            transformActive.DOAnchorPos(EndPositionActive, _speed);
        }
    }
}
