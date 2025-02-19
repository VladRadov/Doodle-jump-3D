using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "BaseGameDataYG", menuName = "ScriptableObject/BaseGameDataYG")]
public class BaseGameDataYG : BaseGameData
{
    public override int CurrentResult { get { return YandexGame.savesData.CurrentResult; } set { YandexGame.savesData.CurrentResult = value; YandexGame.SaveProgress(); } }
    public override int BestResult { get { return YandexGame.savesData.BestResult; } set { YandexGame.savesData.BestResult = value; YandexGame.SaveProgress(); } }
    public override bool IsEducationEnd { get { return YandexGame.savesData.IsEducationEnd; } }
    public override bool IsCatSceneView { get { return YandexGame.savesData.IsCatSceneView; } }

    public override void EndEducation()
    {
        YandexGame.savesData.IsEducationEnd = true;
        YandexGame.SaveProgress();
    }

    public override void EndCatSceneView()
    {
        YandexGame.savesData.IsCatSceneView = false;
        YandexGame.SaveProgress();
    }

    public override void CatSceneView()
    {
        YandexGame.savesData.IsCatSceneView = true;
        YandexGame.SaveProgress();
    }
}
