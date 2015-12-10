using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private ByrdController byrdController;
    [SerializeField]
    private Background backGround;

    [SerializeField]
    private GateController gate;

    private float maxDistanceDifference = 5f;
    private int currentScore = 0;
    #region Unity
    void Awake()
    {
        byrdController.OnByrdDeath += GameOver;
        byrdController.OnByrdScore += PointScored;

        RunStart();
    }
    #endregion

    #region Internal
    void RunStart()
    {
        backGround.CanMove = true;

        //TODO - Setup Byrd

        float yPos = Random.Range(-7f, 10.5f);
        gate.SetFirstPosition(new Vector3(-20, yPos, 0));
    }

    void PointScored(GameObject _gate)
    {
        currentScore++;

        SetNextGate();
    }

    void SetNextGate()
    {
        float yPos = Mathf.Clamp(Random.Range((gate.NextYHeight - maxDistanceDifference), (gate.NextYHeight + maxDistanceDifference)), -7f, 10.5f);
        gate.SetNextConfiguration(yPos);
    }
    void GameOver()
    {
        backGround.CanMove = false;
        gate.GameOver();
    }
    #endregion
}