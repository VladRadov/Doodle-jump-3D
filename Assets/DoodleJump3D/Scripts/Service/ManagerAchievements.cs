using System.Collections.Generic;
using UnityEngine;

public class ManagerAchievements : BaseManager
{
    [SerializeField] private List<Achivement> _achivements;
    [SerializeField] private List<AchivementView> _achivementsItem;

    public override void Initialize()
    {
        _achivements.Sort((Achivement achivement1, Achivement achivement2) => { return achivement1.LevelAchivement < achivement2.LevelAchivement ? achivement1.LevelAchivement : achivement2.LevelAchivement; });
        
        foreach (var achivement in _achivements)
        {
            if (achivement.IsAchivementSuccess == false)
            {
                var findedAchivements = _achivements.FindAll(achivementTemp => achivementTemp.LevelAchivement == achivement.LevelAchivement);
                
                for (int i = 0; i < _achivementsItem.Count; i++)
                {
                    _achivementsItem[i].SetDescriptionAchievement(findedAchivements[i].Description);
                    _achivementsItem[i].SetProgressAchievement(findedAchivements[i].CurrentCountSuccess);
                    _achivementsItem[i].SetMaxProgressAchievement(findedAchivements[i].MaxCountSuccess);
                }

                return;
            }
        }
    }
}
