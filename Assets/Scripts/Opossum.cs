using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    Rigidbody2D rig;
    public float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        rig = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 front = new Vector3(-1, 0, 0);
        if (transform.localScale.x < 0)
        {
            front = new Vector3(1, 0, 0);
        }
        Vector3 begin = transform.position + new Vector3(0,-0.1f,0); //前方向，进行-0.1f位置偏高的修正
        Vector3 down = new Vector3(0,-1,0);//下方向

        //碰到墙壁调头
        Debug.DrawLine(begin,begin+front*0.8f,Color.red);//打印一个射线来辅助调试
        if(Physics2D.Raycast(begin,front,0.8f,LayerMask.GetMask("Default"))){
            transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        }

        //前有悬崖就掉头
        Debug.DrawLine(begin,begin+(front + down).normalized*1.5f,Color.red);//打印一个射线来辅助调试
        if(!Physics2D.Raycast(begin,front + down,1.5f,LayerMask.GetMask("Default"))){
            transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        }



        rig.velocity = new Vector2(speed * front.x, rig.velocity.y);
    }
}
