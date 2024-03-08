using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    public GameObject target;
    public BattleSceneCtrl _baseGameScript;

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattleSceneCtrl>();
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
            Destroy(target.transform.parent.gameObject);
            target = null;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
}
