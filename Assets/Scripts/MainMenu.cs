using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip selectAudio;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Start() {
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic() {

        bgmSource.clip = mainMenuMusic;
        bgmSource.loop = true;

        bgmSource.volume = 0f;
        bgmSource.Play();

        float duration = 2f; // fade time
        float timer = 0f;

        while (timer < duration) {

            timer += Time.deltaTime;

            bgmSource.volume = timer / duration;

            yield return null;
        }

        bgmSource.volume = 1f;
    }

    public void PlayOptionSelect() {
        sfxSource.PlayOneShot(selectAudio, 1f);
    }

    public void PlayGame() {
        PlayOptionSelect();
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame() {
        PlayOptionSelect();
        Application.Quit();
    }
}
