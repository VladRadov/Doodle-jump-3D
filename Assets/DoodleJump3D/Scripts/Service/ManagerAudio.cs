using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAudio : BaseManager
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceEnemy;
    [SerializeField] private AudioSource _audioSourceRocket;
    [SerializeField] private AudioSource _audioSourceOther;
    [SerializeField] private AudioSource _audioSourcePlatform;
    [SerializeField] private List<Audio> _sounds;

    public override void Initialize()
    {
        _audioSource.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
        _audioSourceEnemy.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
        _audioSourceRocket.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
        _audioSourceOther.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
        _audioSourcePlatform.volume = DataSettingsContainer.Instance.Settings.VolumeSounds;
    }

    public void ChangeVolume(float volume)
    {
        _audioSource.volume = volume;
        _audioSourceEnemy.volume = volume;
        _audioSourceRocket.volume = volume;
        _audioSourceOther.volume = volume;
        _audioSourcePlatform.volume = volume;

        DataSettingsContainer.Instance.Settings.VolumeSounds = volume;
    }

    public void PlayJump() => Play(_audioSource, "Jump", false);

    public void PlayShotDoodle() => Play(_audioSource, "Shot", false);

    public void PlayRotateDoodle() => Play(_audioSource, "RotateDoodle", false);

    public void PlayFall() => Play(_audioSource, "Fall", false);

    public void PlaySoundRocket() => Play(_audioSourceRocket, "Rocket", false);

    public void StopSoundRocket() => _audioSourceRocket.Stop();

    public void PlayEnemySound() => Play(_audioSourceEnemy, "EnemySound", true);

    public void PlayEnemyDieSound() => Play(_audioSourceEnemy, "EnemyDie", false);

    public void PlayExplodingPlatform() => Play(_audioSourceOther, "ExplodingPlatform", false);

    public void PlaySoundGetStar() => Play(_audioSourceOther, "GetStar", false);

    public void PlaySoundFallPlatform() => Play(_audioSourcePlatform, "FallPlatform", false);

    public void PlaySoundChangePanelMenu() => Play(_audioSourcePlatform, "ChangePanelMenu", false);

    public void PlaySoundWhitePlatformHide() => Play(_audioSourcePlatform, "WhitePlatformHide", false);

    public void PlaySoundStartGame() => Play(_audioSourceOther, "StartGame", false);

    public void PlayDestructionTitleNameGame() => Play(_audioSourceOther, "DestructionTitleNameGame", true);

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
