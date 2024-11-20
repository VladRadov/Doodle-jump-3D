using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "ScriptableObject/Audio")]
public class Audio : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _audioClip;

    public string Name => _name;
    public AudioClip AudioClip => _audioClip;
}
