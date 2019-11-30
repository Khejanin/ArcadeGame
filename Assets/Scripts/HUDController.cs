using Character;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    private GameLogic GameLogic;

    public Image POName;
    private PlayerController PO;
    public Image POForegroundHP;
    public Image POForegroundCharge;

    public Image PTName;
    private PlayerController PT;
    public Image PTForegroundHP;
    public Image PTForegroundCharge;

    public GameObject gameWonOverlay;
    public GameObject dimCanvas;
    public TMPro.TextMeshProUGUI countDownText;

    public Image value1, value2, value3;
    private int valueCounter = 0;
    private Character.PlayerController.PlayerEnum firstRoundWinner;

    private bool countDownOver = false;

    private float maxCountDownTime = 4f;
    private float countdowntime = 4f;

    public Image winnerName;

    // Start is called before the first frame update
    void Start()
    {
        GameLogic = GameObject.Find("GameManager").GetComponent<GameLogic>();
        //POName.text = "Player One";
        PO = GameLogic.playerLeft.GetComponent<Character.PlayerController>();
        //PTName.text = "Player Two";
        PT = GameLogic.playerRight.GetComponent<Character.PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdowntime > 1)
        {
            countdowntime -= Time.deltaTime;
            countDownText.text = Mathf.Floor(countdowntime).ToString();
        }
        else if (countdowntime > -0)
        {
            countdowntime -= Time.deltaTime;
            countDownText.text = "GO";
        }
        else if (!countDownOver)
        {
            dimCanvas.SetActive(false);
            countDownText.text = "";
            GameLogic.GameState.running = true;
            countDownOver = true;
        }
        else
        {
            float poPercentage = PO.health * 1.0f / PO.maxHealth;
            if (poPercentage < 0)
            {
                poPercentage = 0;
            }
            float ptPercentage = PT.health * 1.0f / PT.maxHealth;
            if (ptPercentage < 0)
            {
                ptPercentage = 0;
            }
            float poChargePercentage = PO.ability.getChargePercentage();
            float ptChargePercentage = PT.ability.getChargePercentage();
            POForegroundHP.rectTransform.localScale = new Vector3(poPercentage, 1, 1);
            PTForegroundHP.rectTransform.localScale = new Vector3(ptPercentage, 1, 1);
            POForegroundCharge.rectTransform.localScale = new Vector3(poChargePercentage, 1, 1);
            PTForegroundCharge.rectTransform.localScale = new Vector3(ptChargePercentage, 1, 1);

            if (GameLogic.GameState.gameOver) gameOver();
        }
    }

    public void roundOver(Character.PlayerController.PlayerEnum playerWon)
    {
        if(valueCounter == 0)
        {
            if (playerWon == Character.PlayerController.PlayerEnum.PlayerLeft) value1.color = new Color(255, 0, 0, 255);
            else value1.color = new Color(0, 0, 255, 255);
        }
        else if (valueCounter == 1)
        {
            if (playerWon == Character.PlayerController.PlayerEnum.PlayerLeft) value2.color = new Color(255, 0, 0, 255);
            else value2.color = new Color(0, 0, 255, 255);
        }
        else if (valueCounter == 2)
        {
            if (playerWon == Character.PlayerController.PlayerEnum.PlayerLeft) value3.color = new Color(255, 0, 0, 255);
            else value3.color = new Color(0, 0, 255, 255);
        }
        valueCounter++;
        if(!GameLogic.GameState.gameOver) resetCounter();
    }

    private void resetCounter()
    {
        countDownOver = false;
        countdowntime = maxCountDownTime;
        GameLogic.GameState.running = false;
        dimCanvas.SetActive(true);
    }

    public void gameOver()
    {
        dimCanvas.SetActive(true);
        gameWonOverlay.SetActive(true);
        GameLogic.GameState.running = false;
        GameLogic.GameState.playerOnePrefab = GameLogic.GameState.PlayerCharactersEnum.None;
        GameLogic.GameState.playerTwoPrefab = GameLogic.GameState.PlayerCharactersEnum.None;
        Invoke(nameof(gotoMenu),10f);
    }

    private void gotoMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
