using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
    GameObject WoodObj;
    float speed=2f;
    void Start()
    {
       WoodObj = GameObject.Find("WoodHand");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wood")
        {
            WoodObj.gameObject.transform.localScale += new Vector3(0, Mathf.Lerp(1.3f, 1f, Time.deltaTime * speed), 0);      
            Destroy(other.gameObject);
        }
    }
}
