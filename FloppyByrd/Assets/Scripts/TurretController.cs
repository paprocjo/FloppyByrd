using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float shotTimer;

    private bool canFire = false;
    public bool CanFire
    {
        get { return canFire; }
        set { canFire = value; }
    }
    private bool isVisible = false;
    private bool targetWithRange;
    private FiringPattern firingPattern = FiringPattern.Single;
    private float timeTillNextShot;
    private int currentProjectile = 0;
    private int projectileCount = 2;
    private List<Projectile> projectiles = new List<Projectile>();

    #region Unity
    private void Awake()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectiles.Add(projectile.GetComponent<Projectile>());
            projectile.GetComponent<Projectile>().Initalize(transform);
        }

        timeTillNextShot = -1;
    }

    private void Update()
    {
        if(isVisible)
        {
            timeTillNextShot -= Time.deltaTime;
            if (timeTillNextShot < 0 && canFire)
            {
                if (targetWithRange)
                {
                    switch (firingPattern)
                    {
                        case FiringPattern.Single:
                            projectiles[currentProjectile].FireProjectile(firingPattern);
                            break;
                    }

                    currentProjectile++;

                    if (currentProjectile > projectileCount - 1)
                        currentProjectile = 0;
                    timeTillNextShot = shotTimer;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            targetWithRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            targetWithRange = false;
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }
    private void OnBecameInvisible()
    {
        isVisible = false;
    }
    #endregion
    #region Internal
    #endregion
    #region Exposed
    public void SetTurretType(FiringPattern _newPattern)
    {
        firingPattern = _newPattern;
    }

    public void RunOver()
    {
        canFire = false;

        foreach(Projectile p in projectiles)
            p.RunOver();
    }
    #endregion
}
public enum FiringPattern
{
    Single,
    Tracking,
    Scatter
}