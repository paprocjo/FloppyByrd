using UnityEngine;
using System.Collections;
using System;

public class ByrdController : MonoBehaviour 
{
    public Action OnByrdDeath;
    public Action<GameObject> OnByrdScore;
    public float flapStrength;

    private bool canFly = true;
    #region Unity
    void Update()
    {
        if (Input.GetKeyDown("space") && canFly)
            Flap();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ScoreZone")
        {
            ByrdDeath();
        }
        else
            if (OnByrdScore != null)
                OnByrdScore(other.transform.parent.gameObject);
    }
    void OnCollisionEnter(Collision other)
    {
        ByrdDeath();
    }
    #endregion

    #region Internal
    private void Flap()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * flapStrength);

        StartCoroutine(PlayFlap());
    }
    private void ByrdDeath()
    {
        canFly = false;

        StartCoroutine(PlayDeath());

        if (OnByrdDeath != null)
            OnByrdDeath();
    }

    //Animations
    IEnumerator PlayDeath()
    {
        GetComponent<Animation>().Play("Impact");
        yield return new WaitForSeconds(GetComponent<Animation>()["Impact"].length);
        GetComponent<Animation>().Play("BounceBack");
        yield return new WaitForSeconds(GetComponent<Animation>()["BounceBack"].length);
        GetComponent<Animation>().Play("Fall");
    }

    IEnumerator PlayFlap()
    {
        GetComponent<Animation>().Play("FlapDown");
        yield return new WaitForSeconds(GetComponent<Animation>()["FlapDown"].length);
        GetComponent<Animation>().Play("FlapUp");
        yield return new WaitForSeconds(GetComponent<Animation>()["FlapUp"].length);
        GetComponent<Animation>().Play("Fall");
    }
    #endregion

    #region Exposed
    #endregion
}