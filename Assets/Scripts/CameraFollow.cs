using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed;
    void Update()
    {
        if (CharacterMovement.startbool == true)
        {
            transform.Translate(new Vector3(0, 0, speed) * speed * Time.deltaTime);
        }
        if (gameObject.transform.position.z > 785)
        {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 785);
        }
    }
}
