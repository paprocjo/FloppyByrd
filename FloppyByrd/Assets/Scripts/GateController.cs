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

    private float nextYHeight = 0;
    public float NextYHeight
    {
        get { return nextYHeight; }
    }

    private bool canMove = true;
    private bool isTop = false;
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

        this.transform.localPosition = new Vector3(-20, nextYHeight, 0);
    }
    #endregion
    #region Exposed
    public void SetFirstPosition(Vector3 _newPosition)
    {
        transform.position = _newPosition;

        isTop = (_newPosition.y >= 0);

        topPipe.SetActive(isTop);
        bottomPipe.SetActive(!isTop);

        canMove = true;
    }
    public void SetNextConfiguration(float _newHeight)
    {
        nextYHeight = _newHeight;
        isTop = (_newHeight >= 0);
    }

    public void GameOver()
    {
        canMove = false;
    }
    #endregion
}