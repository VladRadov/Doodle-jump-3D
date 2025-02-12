using UnityEngine;

using DG.Tweening;
using Cysharp.Threading.Tasks;

public class AnimationPanel
{
    private readonly float _speed;

    public AnimationPanel(float speed)
    {
        _speed = speed;
    }

    public void MoveDOAchorPosActiveObject(GameObject gameObjectActive, Vector2 endPositionActive)
    {
        gameObjectActive?.SetActive(true);

        if (gameObjectActive != null)
        {
            var transformActive = gameObjectActive.GetComponent<RectTransform>();
            transformActive.DOAnchorPos(endPositionActive, _speed);
        }
    }

    public async UniTask MoveDOAchorPosHideObject(GameObject gameObjectHide, Vector2 endPositionHide)
    {
        if (gameObjectHide != null)
        {
            var transformHide = gameObjectHide.GetComponent<RectTransform>();
            transformHide.DOAnchorPos(endPositionHide, _speed);
            await UniTask.Delay(500);

            gameObjectHide.SetActive(false);
        }
    }
}
