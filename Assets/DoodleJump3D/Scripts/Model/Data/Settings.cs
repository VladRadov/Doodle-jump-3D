using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Settings")]
public class Settings : ScriptableObject
{
    [Range(0, 1)]
    [SerializeField] private float _volumeSounds;

    [SerializeField] private int _delayAfterDieDoodle;

    public float VolumeSounds { get { return _volumeSounds; } set { _volumeSounds = value; } }

    public int DelayAfterDieDoodle { get { return _delayAfterDieDoodle; } set { _delayAfterDieDoodle = value; } }
}
