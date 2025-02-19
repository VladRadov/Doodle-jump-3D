using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "SettingsYG", menuName = "ScriptableObject/SettingsYG")]
public class SettingsYG : Settings
{
    public override float VolumeSounds { get { return YandexGame.savesData.VolumeSounds; } set { YandexGame.savesData.VolumeSounds = value; YandexGame.SaveProgress(); } }
    public override int DelayAfterDieDoodle { get { return YandexGame.savesData.DelayAfterDieDoodle; } set { YandexGame.savesData.DelayAfterDieDoodle = value; YandexGame.SaveProgress(); } }
}
