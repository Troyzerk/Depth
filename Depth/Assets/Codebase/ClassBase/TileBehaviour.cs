using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TileBehaviour : MonoBehaviour
{
    public bool activityCheck = false;
    public GameObject spaceInvader;

    public BattleSceneCtrl _baseGameScript;

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattleSceneCtrl>();
        SetAnim(false);
    }

    private void Update()
    {
        if(_baseGameScript.onTile)
        {
            SetAnim(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Detection")
        {
            if (this.transform.GetChild(0).gameObject != null)
            {
                SetAnim(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Detection")
        {
            SetAnim(true);
        }
    }

   private void SetAnim(bool set)
    {
        transform.GetChild(0).gameObject.SetActive(set);
        transform.GetChild(1).gameObject.SetActive(set);
        if (transform.GetChild(1).gameObject.activeInHierarchy) 
        {
            transform.GetChild(1).GetComponent<Animator>().SetBool("Play", set);
        }
        
        spaceInvader = null;
    }
}
