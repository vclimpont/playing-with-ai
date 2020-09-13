using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text textCoin = null;
    [SerializeField] private Text textLevel = null;
    [SerializeField] private Text textScore = null;
    [SerializeField] private Map map = null;

    private void Start()
    {
        textLevel.text = "LEVEL " + Level.GetCurrentLevel();
        textCoin.text = "0 / " + (map.numberOfCoins + Level.GetCurrentLevel());
        UpdateTextScore();
    }

    public void SetTextCoin(string s)
    {
        textCoin.text = s;
    }

    public void UpdateTextScore()
    {
        textScore.text = "" + Level.GetScore();
    }

    public void EndGame()
    {
        Restart();
    }

    public void GoToNextLevel()
    {
        Level.SetCurrentLevel(Level.GetCurrentLevel() + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Restart()
    {
        Level.SetCurrentLevel(1);
        Level.SetScore(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
