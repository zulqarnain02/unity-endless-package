using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;

    public GameObject startText;

    public static int numberOfCoins;
    public Text coinsText;

    void Start()
    {
        gameOver=false;
        Time.timeScale=1;
        isGameStarted=false;
        numberOfCoins=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver){
            Time.timeScale=0;
            gameOverPanel.SetActive(true);
        }

        coinsText.text="Coins: " + numberOfCoins;

        if(SwipeManager.tap){
            isGameStarted=true;
            Destroy(startText);
        }
    }


}
