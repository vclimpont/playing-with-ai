using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text textCoin = null;
    [SerializeField] private Text textLevel = null;
    [SerializeField] private Map map = null;

    private void Start()
    {
        textLevel.text = "LEVEL " + Level.GetCurrentLevel();
        textCoin.text = "0 / " + map.numberOfCoins;
    }

    public void SetTextCoin(string s)
    {
        textCoin.text = s;
    }

    public void EndGame()
    {
        Restart();
    }

    void Restart()
    {
        Level.SetCurrentLevel(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
