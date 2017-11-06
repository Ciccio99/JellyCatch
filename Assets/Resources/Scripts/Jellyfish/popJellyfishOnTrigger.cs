using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popJellyfishOnTrigger : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Jellyfish")
        {
            collision.gameObject.GetComponent<jellyPopScript>().popJellyfish();
        }
    }
}
