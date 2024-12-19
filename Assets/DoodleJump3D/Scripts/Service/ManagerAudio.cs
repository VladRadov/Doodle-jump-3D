using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAudio : BaseManager
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceEnemy;
    [SerializeField] private AudioSource _audioSourceRocket;
    [SerializeField] private List<Audio> _sounds;

    public override void Initialize()
    {
        _audioSource.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
        _audioSourceEnemy.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
        _audioSourceRocket.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
    }

    public void ChangeVolume(float volume)
    {
        _audioSource.volume = volume;
        _audioSourceEnemy.volume = volume;
        _audioSourceRocket.volume = volume;

        DataSettingsContainer.Instance.Settings.VolumeSounds = volume;
    }

    public void PlayJump() => Play(_audioSource, "Jump", false);

    public void PlayFall() => Play(_audioSource, "Fall", false);

    public void PlayRocket() => Play(_audioSourceRocket, "Rocket", false);

    public void PlayEnemySound() => Play(_audioSourceEnemy, "EnemySound", true);

    private void Play(AudioSource player, string nameAudio, bool isLoop)
    {
        var sound = FindAudio(nameAudio);
        if (sound is AudioNull)
            return;

        player.loop = isLoop;
        player.clip = sound.AudioClip;
        player.Play();
    }

    private Audio FindAudio(string nameSound)
    {
        var findedSound = _sounds.Find(sound => sound.Name == nameSound);
        return findedSound != null ? findedSound : new AudioNull();
    }
}
