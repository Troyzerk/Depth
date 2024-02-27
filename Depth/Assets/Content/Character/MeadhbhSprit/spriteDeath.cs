using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteDeath : MonoBehaviour
{
    [SerializeField] private float coolDown;
    private void Awake()
    {
        StartCoroutine(DestroySelf(coolDown));
    }

    private IEnumerator DestroySelf(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
