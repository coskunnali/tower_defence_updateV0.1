using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI live;

    public static bool gameOver;
    public Image gameOverBG;
    public TextMeshProUGUI gameOverTXT;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        money.text = "Money: " + PlayerState.Money.ToString();
        live.text = "Live : " + PlayerState.Live.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        money.text = "Money: " + PlayerState.Money.ToString();
        if (PlayerState.Live >= 0) 
        {
            live.text = "Live : " + PlayerState.Live.ToString();
        }

        if (PlayerState.Live <= 0)
        {
            gameOver = true;
            gameOverBG.gameObject.SetActive(true);
            gameOverTXT.gameObject.SetActive(true);

        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
