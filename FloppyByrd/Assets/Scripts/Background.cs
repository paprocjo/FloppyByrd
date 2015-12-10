using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour 
{
    [SerializeField]
    private Transform mountains;

    private float groundSpeed = 10;
    private float mountainSpeed = 5;

    #region Unity
	void Update () 
    {
        transform.Rotate(Vector3.up * Time.deltaTime * groundSpeed);
        mountains.Rotate(Vector3.down * Time.deltaTime * mountainSpeed);
    }
    #endregion
}