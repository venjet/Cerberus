using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMotion : MonoBehaviour {

    Rigidbody2D rig;
    
    // Start is called before the first frame update
    void Start () {
        rig = transform.GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {

    }

    private void FixedUpdate () {
        transform.Rotate(0,0,-4);
        transform.position = new Vector3 (transform.position.x + 0.08f, transform.position.y, transform.position.z);
    }
}