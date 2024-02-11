using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    public GameObject target;
    bool killYourSelf;
    public BaseGameScript _baseGameScript;

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BaseGameScript>();
    }

    private void Update()
    {
        if (_baseGameScript.iSee == true)
        {
            if (target == null)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        target = collision.gameObject;
        if (_baseGameScript.iSee == true)
        {
            Destroy(target);
            target = null;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
        if (_baseGameScript.iSee == true)
        {
            Destroy(target);
            target = null;
        }
    }
}
