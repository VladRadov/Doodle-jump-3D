using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Settings")]
public class Settings : ScriptableObject
{
    [Range(0, 1)]
    [SerializeField] private float _volumeSounds;

    public float VolumeSounds { get { return _volumeSounds; } set { _volumeSounds = value; } }
}
