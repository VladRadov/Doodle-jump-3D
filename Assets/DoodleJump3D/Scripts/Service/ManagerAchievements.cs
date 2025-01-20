using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ManagerAchievements : BaseManager
{
    private AnimationPanel _animationPanel;

    [SerializeField] private List<Achivement> _achivements;
    [SerializeField] private List<AchivementView> _achivementsItem;

    public override void Initialize()
    {
        _animationPanel = new AnimationPanel(0.3f);

        LoadingNoCompletedAchivements();
    }

    private async void LoadingNoCompletedAchivements()
    {
        var findedNoCompletedAchivements = FindNoCompletedAchivements();

        if (findedNoCompletedAchivements.Count == 0)
            return;

        for (int i = 0; i < _achivementsItem.Count; i++)
        {
            if (i >= findedNoCompletedAchivements.Count)
                return;

            _achivementsItem[i].SetDescriptionAchievement(findedNoCompletedAchivements[i].Description);
            _achivementsItem[i].SetProgressAchievement(findedNoCompletedAchivements[i].CurrentCountSuccess);
            _achivementsItem[i].SetMaxProgressAchievement(findedNoCompletedAchivements[i].MaxCountSuccess);

            if(i == 0 )
                _animationPanel.MoveDOAchorPosActiveObject(_achivementsItem[i].gameObject, new Vector2(-98, 81));
            if (i == 1)
                _animationPanel.MoveDOAchorPosActiveObject(_achivementsItem[i].gameObject, new Vector2(-81, 10));
            if (i == 2)
                _animationPanel.MoveDOAchorPosActiveObject(_achivementsItem[i].gameObject, new Vector2(-128, -60));

            await UniTask.Delay(500);
        }
    }

    private List<Achivement> FindNoCompletedAchivements()
    {
        SortAchivementAscendingLevel();
        List<Achivement> findedAchivements = new();

        foreach (var achivement in _achivements)
        {
            if (achivement.IsAchivementSuccess)
                continue;

            findedAchivements = FindAchivements(achivement.LevelAchivement);

            if (findedAchivements != null)
                break;
        }

        return findedAchivements;
    }

    private List<Achivement> FindAchivements(int levelAchivement)
        => _achivements.FindAll(achivementTemp => achivementTemp.LevelAchivement == levelAchivement);

    private void SortAchivementAscendingLevel()
    {
        _achivements.Sort((Achivement achivement1, Achivement achivement2) =>
        {
            return achivement1.LevelAchivement < achivement2.LevelAchivement ? achivement1.LevelAchivement : achivement2.LevelAchivement;
        });
    }
}
