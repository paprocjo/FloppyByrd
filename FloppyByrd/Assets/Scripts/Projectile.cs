using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int speed;
    [SerializeField]
    private bool isFired = false;

    private float timeTillLifeOver;
    private FiringPattern firingPattern = FiringPattern.Single;
    private Transform origin;
    private Transform player;
    private Vector3 targetLocation;

    #region Unity
    private void Update()
    {
        if (timeTillLifeOver < 0 && isFired)
        {
            timeTillLifeOver -= Time.deltaTime;
            switch(firingPattern)
            {
                case FiringPattern.Single:
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    break;
            }
        }
        else if( timeTillLifeOver >= 0)
        {
            ResetProjectile();
        }
    }
    private void OnBecameInvisible()
    {
        ResetProjectile();
    }
    #endregion
    #region Internal
    private void ResetProjectile()
    {
        isFired = false;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Renderer>().enabled = false;
    }
    #endregion
    #region Exposed
    public void Initalize(Transform _origin)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        origin = _origin;
        timeTillLifeOver = -2;
        transform.parent = origin;
    }
    public void FireProjectile(FiringPattern _Pattern, Vector3 _targetLocation = new Vector3())
    {
        transform.position = origin.position;

        //Single Shot With modified X for closer shot
        if (_targetLocation == new Vector3())
            targetLocation = new Vector3(player.position.x - 3.5f, player.position.y, 0f);
        else
            targetLocation = _targetLocation;

        transform.LookAt(targetLocation);

        isFired = true;

        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<Renderer>().enabled = true;
    }
    public void RunOver()
    {
        ResetProjectile();
    }
    #endregion
}