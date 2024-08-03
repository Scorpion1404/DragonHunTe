using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiGameCllear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject gameClearScreen;
    [SerializeField] private AudioClip gameClearSound;


    private void Awake()
    {
        gameClearScreen.SetActive(false);
    }
    public void GameClear()
    {
        gameClearScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameClearSound);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}

