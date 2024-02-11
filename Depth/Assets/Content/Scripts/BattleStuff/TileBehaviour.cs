using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{

    public GameObject hoverOverOutline;
    public GameObject hoverOutOutlinePrefab;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Detection")
        {
            if (this.transform.GetChild(0).gameObject != null)
            {
                Destroy(this.transform.GetChild(0).gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Detection")
        {
            Vector3 worldPosition = this.transform.position;
            hoverOutOutlinePrefab = Instantiate(hoverOverOutline, worldPosition, Quaternion.identity, this.transform);
        }
    }
}
