using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAudio : BaseManager
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceEnemy;
    [SerializeField] private List<Audio> _sounds;

    public override void Initialize()
    {

    }

    public void PlayJump() => Play(_audioSource, "Jump", false);

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
