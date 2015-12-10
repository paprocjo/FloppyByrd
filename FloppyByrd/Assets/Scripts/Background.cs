using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour 
{
    [SerializeField]
    private Transform mountains;

    private float groundSpeed = 5;
    private float mountainSpeed = 5;

    private bool canMove = true;
    public bool CanMove
    { set { canMove = value; } }

    #region Unity
	void Update () 
    {
        if (canMove)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * groundSpeed);
            mountains.Rotate(Vector3.down * Time.deltaTime * mountainSpeed);
        }
    }
    #endregion
}