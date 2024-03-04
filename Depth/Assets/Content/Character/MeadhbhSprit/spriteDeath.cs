using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteDeath : MonoBehaviour
{
    public float coolDown;

    public IEnumerator DestroySelf(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.transform.position = new Vector3(0, 0, 0);
        this.gameObject.SetActive(false);
    }
}
