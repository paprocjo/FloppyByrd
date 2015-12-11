using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private GameObject bottomPipe;
    [SerializeField]
    private GameObject topPipe;
    [SerializeField]
    private TurretController bottomTurret;
    [SerializeField]
    private TurretController topTurret;

    [SerializeField]
    private GameObject topPowerUp;
    [SerializeField]
    private GameObject bottomPowerUp;

    private float nextYHeight = 0;
    public float NextYHeight
    {
        get { return nextYHeight; }
    }

    private bool canMove = false;
    private bool isTop = false;
    private bool hasPowerUp = false;
    private float pipeSpeed = 15;

    #region Unity
	void Update () 
    {
        if (canMove)
            transform.Translate(Vector3.right * Time.deltaTime * pipeSpeed);

        if (this.transform.position.x > 20)
            ResetOnRightSide();
    }
    #endregion
    #region Internal
    private void ResetOnRightSide()
    {
        topPipe.SetActive(isTop);
        bottomPipe.SetActive(!isTop);

        if (hasPowerUp)
        {
            if (isTop)
                topPowerUp.SetActive(true);
            else
                bottomPowerUp.SetActive(true);
        }
        else
        {
            topPowerUp.SetActive(false);
            bottomPowerUp.SetActive(false);
        }

        this.transform.localPosition = new Vector3(-20, nextYHeight, 0);
    }
    private void SetupTurrets() 
    {
        if (isTop)
            topTurret.SetTurretType(FiringPattern.Single);
        else
            bottomTurret.SetTurretType(FiringPattern.Single);

        topTurret.CanFire = isTop;
        bottomTurret.CanFire = !isTop;
    }
    #endregion
    #region Exposed
    public void SetFirstPosition(Vector3 _newPosition)
    {
        transform.position = _newPosition;

        isTop = (_newPosition.y >= 0);

        topPipe.SetActive(isTop);
        bottomPipe.SetActive(!isTop);

        SetupTurrets();
        
        canMove = true;
    }
    public void SetNextConfiguration(float _newHeight)
    {
        nextYHeight = _newHeight;
        isTop = (_newHeight >= 0);

        hasPowerUp = false;
        int x = Random.Range(0, 41);

        if(x == 0 || x == 40)
            hasPowerUp = true;

        SetupTurrets();
    }

    public void GameOver()
    {
        canMove = false;
        topTurret.RunOver();
        bottomTurret.RunOver();
    }
    #endregion
}