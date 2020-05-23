using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun {
    CharacterController2D cc;
    public float speed = 5;
    float move;
    bool jump;
    bool isDead = false;
    Animator ani;
    Rigidbody2D rig;
    Transform stampPoint;

    public GameObject deathEff;
    private GameObject playDeathEff;

    // Start is called before the first frame update
    void Start () {
        cc = GetComponent<CharacterController2D> ();
        ani = GetComponent<Animator> ();
        rig = GetComponent<Rigidbody2D> ();
        stampPoint = transform.Find ("StampPoint");
    }

    // Update is called once per frame
    void Update () {

        if (!isDead) {
            GameData gameData =  GameObject.FindWithTag("GameData").GetComponent<GameData>();
        
            if(gameData.gameMode ==  2){
                if(!photonView.IsMine && PhotonNetwork.IsConnected)
                return;
            }

            move = ETCInput.GetAxis ("Horizontal");
            move *= speed;
            if (cc.isGrounded) {
                ani.SetFloat ("speed", Mathf.Abs (move));
                ani.SetBool ("isUp", false);
                ani.SetBool ("isDown", false);
            } else {
                Vector3 vel = rig.velocity;
                if (vel.y > 0) {
                    ani.SetBool ("isUp", true);
                    ani.SetBool ("isDown", false);
                } else {
                    ani.SetBool ("isUp", false);
                    ani.SetBool ("isDown", true);
                }
            }
            jump = ETCInput.GetButton ("Jump");
        }
    }

    private void FixedUpdate () {
        StampCheck ();
        cc.Move (move, jump);
    }

    private void StampCheck () {
        Collider2D cc = Physics2D.OverlapCircle (stampPoint.position, 0.1f, LayerMask.GetMask ("Enemy"));
        if (cc == null) {
            return;
        }
        Debug.Log ("踩到一只" + cc.name);
        //播放死亡特效
        playDeathEff = Instantiate (deathEff);
        playDeathEff.transform.position = cc.transform.position;
        Invoke ("CleanDeathEff", 0.2857f);
        Destroy (cc.gameObject);
        rig.velocity = new Vector2 (rig.velocity.x, 0);
        rig.AddForce (new Vector2 (0, 200));
    }

    private void OnCollisionEnter2D (Collision2D other) {
        //处理箱子层的碰撞
        if (other.gameObject.layer == LayerMask.NameToLayer ("Box")) {
            if (other.transform.position.y >= transform.position.y + 0.1f) {
                OnBunt box = other.transform.GetComponent<OnBunt> ();
                if (box) {
                    box.BeBunted ();
                }
            }
        }
        //处理道具层的碰撞
        if (other.gameObject.layer == LayerMask.NameToLayer ("Item")) {
            Item item = other.transform.GetComponent<Item> ();
            if (item) {
                item.OnItemGot (this);
            }

        }
        //处理怪物层的碰撞
        if (other.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
            isDead = true;
            ani.SetBool ("isHurt", isDead);
            move = 0;
            jump = false;
            rig.velocity = new Vector2 (0, 0);
            rig.AddForce (new Vector2 (0, 450));
            Debug.Log ("awsl");
            Invoke ("Restart", 2);
        }
    }

    private void CleanDeathEff () {
        Destroy (playDeathEff);
    }

    private void Restart () {
        SceneManager.LoadScene (0);
    }

    public void getDogCoin () {
        transform.localScale = new Vector3 (Mathf.Sign (transform.localScale.x), 1, 1) * 1.3f;
        cc.jumpForce = 580;
    }

}