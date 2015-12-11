using UnityEngine;
using System.Collections;
using System;

public class ByrdController : MonoBehaviour 
{
    public Action OnByrdDeath;
    public Action<GameObject> OnByrdScore;
    public Action<string> OnByrdPowerUpCollect;

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
        if (other.gameObject.tag == "ScoreZone")
        {
            if (OnByrdScore != null)
                OnByrdScore(other.transform.parent.gameObject);
        }

        if (other.gameObject.tag == "Multiplier")
        {
            other.gameObject.SetActive(false);

            if (OnByrdScore != null)
                OnByrdPowerUpCollect(other.gameObject.tag);
        }
            
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
    public void ResetByrd()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.transform.position = new Vector3(0f, 15f, 0f);
        this.GetComponent<Rigidbody>().useGravity = true;
        canFly = true;
    }
    #endregion
}