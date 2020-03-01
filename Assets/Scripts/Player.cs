using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Player : MonoBehaviour
{
    CharacterController2D cc;
    public float speed = 5;
    float move;
    bool jump;
    Animator ani;
    Rigidbody2D rig;
    Transform stampPoint;

    public GameObject deathEff;
    private GameObject playDeathEff;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController2D>();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        stampPoint = transform.Find("StampPoint");
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        move *= speed;

        if (cc.isGrounded)
        {
            ani.SetFloat("speed", Mathf.Abs(move));
            ani.SetBool("isUp", false);
            ani.SetBool("isDown", false);
        }
        else
        {
            Vector3 vel = rig.velocity;
            if (vel.y > 0)
            {
                ani.SetBool("isUp", true);
                ani.SetBool("isDown", false);
            }
            else
            {
                ani.SetBool("isUp", false);
                ani.SetBool("isDown", true);
            }
        }

        jump = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        StampCheck();
        cc.Move(move, jump);
    }

    private void StampCheck()
    {
        Collider2D cc = Physics2D.OverlapCircle(stampPoint.position, 0.1f, LayerMask.GetMask("Enemy"));
        if (cc == null)
        {
            return;
        }
        Debug.Log("踩到一只" + cc.name);
        //播放死亡特效
        playDeathEff = Instantiate(deathEff);
        playDeathEff.transform.position = cc.transform.position;
        Invoke("CleanDeathEff", 0.2857f);
        Destroy(cc.gameObject);
        rig.velocity = new Vector2(rig.velocity.x, 0);
        rig.AddForce(new Vector2(0, 200));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            return;
        }
        Debug.Log("awsl");
        Invoke("Restart", 2);
    }

    private void CleanDeathEff()
    {
        Destroy(playDeathEff); 
    }

    private void Restart()
    {
        EditorSceneManager.LoadScene(0);
    }
}
