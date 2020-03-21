using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBunt : MonoBehaviour {
    public Sprite afterSprite;
    public GameObject dogCoin;

    private bool isBunted = false;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void BeBunted () {
        if (!isBunted) {
            //isBunted = true;
            StartCoroutine (DropItem ());
        }
    }

    IEnumerator DropItem () {
        GetComponent<SpriteRenderer> ().sprite = afterSprite;
        for (int i = 0; i < 6; i++) {
            transform.Translate (new Vector3 (0, 0.06f, 0));
            yield return null;
        }

        for (int i = 0; i < 2; i++) {
            transform.Translate (new Vector3 (0, -0.18f, 0));
            yield return null;
        }

        if (dogCoin) {
            GameObject obj = Instantiate (dogCoin, transform.position, Quaternion.identity);
            if (obj.transform.GetComponent<ItemMotion> () != null) {
                obj.transform.GetComponent<ItemMotion>().enabled = false;
                obj.transform.GetComponent<Rigidbody2D> ().gravityScale = 0;
            }
            for (int i = 0; i < 10; i++) {
                obj.transform.Translate (0, 0.1f, 0);
                yield return null;
            }
            obj.transform.GetComponent<ItemMotion>().enabled = true;
            obj.transform.GetComponent<Rigidbody2D> ().gravityScale = 2f;
        }
    }

}