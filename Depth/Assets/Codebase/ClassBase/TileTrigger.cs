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
        gameObject.GetComponent<Collider2D>().enabled = true;
    }


    private void Update()
    {
        if (_baseGameScript.iSee)
        {
            ReMoveMinion();
        }
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        target = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
    }
    private void ReMoveMinion()
    {
        
        if(target != null) 
        {
            Destroy(target.transform.parent.gameObject);
        }
        Destroy(this.gameObject);
    }
}
