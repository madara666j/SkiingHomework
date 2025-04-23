using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Image overlay;

    private void Start()
    {
        overlay.CrossFadeAlpha(0, 0.9f, true);
        gameOverScreen.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.raceEnd += EnableFinishUI;
    }

    private void OnDisable()
    {
        GameEvents.raceEnd -= EnableFinishUI;
    }

    private void EnableFinishUI()
    {
        gameOverScreen.SetActive(true);
    }
    public void RestartLevel()
    {
        StartCoroutine(RestartCoroutine());
    }

    public void NextLevel()
    {

    }

    private IEnumerator RestartCoroutine()
    {
        overlay.CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}