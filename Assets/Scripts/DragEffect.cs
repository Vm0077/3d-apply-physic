using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEffect : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){

        }
    }

    void OnCollisionExit (Collider other)
    {
        if(other.tag == "Player"){

        }

    }
}
