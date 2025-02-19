using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

using Cysharp.Threading.Tasks;

public class ManagerPostProcessProfile : BaseManager
{
    [SerializeField] private PostProcessProfile _baseProfile;

    public override void Initialize()
    {
        var lensDistortion = _baseProfile.GetSetting<LensDistortion>();
        lensDistortion.intensity.value = 0;
    }

    public async void StartRocketEffect()
    {
        var lensDistortion = _baseProfile.GetSetting<LensDistortion>();
        int intensity = -5;
        int stepIntensity = -2;
        int maxIntensity = -75;

        while (intensity > maxIntensity)
        {
            intensity += stepIntensity;
            lensDistortion.intensity.value = intensity;
            await UniTask.Delay(1);
        }
    }

    public async void StopRocketEffect()
    {
        var lensDistortion = _baseProfile.GetSetting<LensDistortion>();
        float intensity = lensDistortion.intensity.value;
        int stepIntensity = 1;
        int maxIntensity = 0;

        while (intensity != maxIntensity)
        {
            intensity += stepIntensity;
            lensDistortion.intensity.value = intensity;
            await UniTask.Delay(1);
        }
    }
}
