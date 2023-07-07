using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cutter : MonoBehaviour
{
    public GameObject karakter;
    public GameObject objesitleme;
    public GameObject woody;
    public void Cut(Transform cutter)
    {
        if (cutter.transform.position.x < 0)//sawlarýn konumuna bakýyoruz -x'te mi +x'te mi 
        {
            
            float y = transform.localScale.y;//bu silindirimizin woodumuzun localscalede y kýsmý
            y -= transform.position.x;//y'den position'xi çýkar diyoruz yani 0ý çýkarýyor gibi oluyoruz
            float dist = y + cutter.position.x;
            
            if (dist / 2 > 0)//distancemin 2ye bölünmesi 0dan büyükse küçük þeyleri de algýlamasýn diye yapýyoruz
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - dist / 2, transform.localScale.z);
                transform.position = new Vector3(transform.position.x + dist / 2, transform.position.y, transform.position.z);
                GameObject prime = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                prime.SetActive(false);
                prime.transform.localScale = new Vector3(transform.localScale.x, dist / 2, transform.localScale.z);
                prime.transform.rotation = transform.rotation;
                prime.transform.position = new Vector3(-(y - prime.transform.localScale.y), transform.position.y, transform.position.z);

                prime.AddComponent<Rigidbody>();
                StartCoroutine(Esit());
            }
        }
        else
        {       
            float y = transform.localScale.y;
            y += transform.position.x;
            float dist = y - cutter.position.x;
            if (dist / 2 > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - dist / 2, transform.localScale.z);
                transform.position = new Vector3(transform.position.x - dist / 2, transform.position.y, transform.position.z);
                GameObject prime = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                prime.SetActive(false);
                prime.transform.localScale = new Vector3(transform.localScale.x, dist / 2, transform.localScale.z);
                prime.transform.rotation = transform.rotation;
                prime.transform.position = new Vector3(y - prime.transform.localScale.y, transform.position.y, transform.position.z);

                prime.AddComponent<Rigidbody>();
                StartCoroutine(Esit());
            }      
        }      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Saw")
        {
            Cut(other.gameObject.transform);       
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        
        Destroy(gameObject);
    }
    IEnumerator Esit()
    {
        yield return new WaitForSeconds(0.1f);
        woody.transform.position = objesitleme.transform.position;
    }
}