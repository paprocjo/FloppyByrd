using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private ByrdController byrdController;
    [SerializeField]
    private Background backGround;

    [SerializeField]
    private GateController gateOne;
    [SerializeField]
    private GateController gateTwo;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text instructionText;
    [SerializeField]
    private Text multiplyerCountText;
    [SerializeField]
    private Button startButton;

    private float maxDistanceDifference = 5f;
    private float multiCountDown = 10f;
    private int currentScore = 0;
    private int multiCount = 0;
    bool isMultiOn = false;

    #region Unity
    void Awake()
    {
        byrdController.OnByrdDeath += GameOver;
        byrdController.OnByrdScore += PointScored;
        byrdController.OnByrdPowerUpCollect += PowerUpCollected;

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if(multiCount >= 1 && isMultiOn == false)
            {
                isMultiOn = true;
                multiCount--;

                multiplyerCountText.text = "Multiplyer Count: " + multiCount;
            }
        }

        if (isMultiOn)
        {
            if (multiCountDown > 1)
                multiCountDown -= Time.deltaTime;
            else
            {
                isMultiOn = false;
                multiCountDown = 10;
            }
        }
    }
    #endregion

    #region Internal
    void RunStart()
    {
        backGround.CanMove = true;

        byrdController.ResetByrd();

        float yPos = Random.Range(-7f, 7f);
        gateOne.SetFirstPosition(new Vector3(-20, yPos, 0));

        yPos = Mathf.Clamp(Random.Range((yPos - maxDistanceDifference), (yPos + maxDistanceDifference)), -7f, 7f);
        gateTwo.SetFirstPosition(new Vector3(-40, yPos, 0));
    }

    void PointScored(GameObject _gate)
    {
        if (isMultiOn)
            currentScore++;
        
        currentScore++;
        scoreText.text = "Score: " + currentScore;

        SetNextGate();
    }

    void PowerUpCollected(string _tag)
    {
        if (_tag == "Multiplier")
        {
            multiCount++;
            multiplyerCountText.text = "Multiplyer Count: " + multiCount;
        }
    }

    void SetNextGate()
    {
        float yPos = Mathf.Clamp(Random.Range((gateOne.NextYHeight - maxDistanceDifference), (gateOne.NextYHeight + maxDistanceDifference)), -7f, 7f);
        gateOne.SetNextConfiguration(yPos);

        yPos = Mathf.Clamp(Random.Range((gateOne.NextYHeight - maxDistanceDifference), (gateOne.NextYHeight + maxDistanceDifference)), -7f, 7f);
        gateTwo.SetNextConfiguration(yPos);
    }
    void GameOver()
    {
        backGround.CanMove = false;
        gateOne.GameOver();
        gateTwo.GameOver();

        int x = PlayerPrefs.GetInt("HighScore");
        if (currentScore > x)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreText.text = "High Score: " + currentScore;
        }

        startButton.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
    }
    #endregion

    #region Exposed
    public void PlayClicked()
    {
        currentScore = 0;
        scoreText.text = "Score: " + currentScore;

        multiCount = 0;
        multiplyerCountText.text = "Multiplyer Count: " + multiCount;

        RunStart();

        startButton.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
    }
    #endregion
}