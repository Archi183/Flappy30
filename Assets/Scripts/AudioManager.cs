using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip flapAudio;
    [SerializeField] private AudioClip scoreAudio;
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip selectAudio;
    [SerializeField] private AudioClip gameOverAudio;
    [SerializeField] private AudioClip bgm1Audio;
    [SerializeField] private AudioClip bgm2Audio;
    [SerializeField] private AudioClip mainMenuAudio;
    private Coroutine bgmRoutine;
    private bool isPlaying = false;


    private void PlayBGM1() {
        bgmSource.clip = bgm1Audio;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    private void PlayBGM2() {
        bgmSource.clip = bgm2Audio;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    private IEnumerator BGMCycle() {

        while (isPlaying) {

            // Wait 60 seconds of active gameplay
            float timer = 0f;
            while (timer < 30f && isPlaying) {
                timer += Time.deltaTime;
                yield return null;
            }

            if (!isPlaying) yield break;

            // Play BGM1
            PlayBGM1();

            yield return new WaitForSeconds(bgm1Audio.length);

            if (!isPlaying) yield break;

            // Wait 60 seconds again
            timer = 0f;
            while (timer < 30f && isPlaying) {
                timer += Time.deltaTime;
                yield return null;
            }

            if (!isPlaying) yield break;

            // Play BGM2
            PlayBGM2();

            yield return new WaitForSeconds(bgm2Audio.length);
        }
    }

    public void StartGameplayMusicCycle() {
        isPlaying = true;
        bgmRoutine = StartCoroutine(BGMCycle());
    }

    public void StopGameplayMusicCycle() {
        isPlaying = false;
        if (bgmRoutine != null)
            StopCoroutine(bgmRoutine);

        bgmSource.Stop();
    }

    public void PlayFlap() {
        sfxSource.PlayOneShot(flapAudio, 1f);
    }
    public void PlayScore() {
        sfxSource.PlayOneShot(scoreAudio, 1f);
    }
    public void PlayHit() {
        Debug.Log("PlayerHit played!");
        sfxSource.PlayOneShot(hitAudio, 1f);
    }
    public void PlayOptionSelect() {
        sfxSource.PlayOneShot(selectAudio, 1f);
    }
    
    public void PlayMainMenuMusic() {
        bgmSource.clip = mainMenuAudio;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayGameOver() {
        bgmSource.clip = gameOverAudio;
        bgmSource.loop = false;
        bgmSource.Play();
    }

}
