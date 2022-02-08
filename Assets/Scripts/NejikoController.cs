using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    //stun 気絶 duration 少しの時間
    const float StunDuration = 0.5f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    //charactorControllの時は、自分で重力を設定の必要がある。
    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    public int Life()
    {
        return life;
    }

    bool IsStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        //controller = this.gameObject.GetComponent<CharacterController>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("start:" + targetLane);
        //デバッグ用
        if (Input.GetKeyDown("left"))
        {
            MoveToLeft();
        }
        if (Input.GetKeyDown("right"))
        {
            MoveToRight();
        }
        if (Input.GetKeyDown("space"))
        {
            Jump();
        }

        //
        if (IsStun())
        {
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            //Clampは、挟み込んだ値
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;

        }
        //キャラクターが地面に接地してたら
        //        if (controller.isGrounded)
        //        {
        //            //上を押すと+1、下を押すと-1
        //            if (Input.GetAxis("Vertical") > 0.0f)
        //            {
        //                moveDirection.z = Input.GetAxis("Vertical") * speedZ;
        //            }
        //            else
        //           {
        //                moveDirection.z = 0;
        //            }
        //キャラクターの回転
        //unityは左手系の座標だから右を押すと時計回り
        //            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

        //            if (Input.GetButton("Jump"))
        //            {
        //               moveDirection.y = speedJump;
        //                animator.SetTrigger("jump");
        //            }
        //        }

        //deltaTimeは、1フレームに要する時間。
        //なぜ掛けるかというと、1秒間に６０フレームじゃない環境（例えば古いパソコン）があった場合でも
        //動きが遅くならないようにする。
        moveDirection.y -= gravity * Time.deltaTime;

        //TransformDirectionは、キャラクターが向いた方向のベクトル（つまりz方向）に変換
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        if (controller.isGrounded)
        {
            moveDirection.y = 0;
        }
        animator.SetBool("run", moveDirection.z > 0.0f);
        //controllerは、キャラクターの動きの制御
        //animatorはキャラクターの見た目の制御
    }

    public void MoveToLeft()
    {
        /*
        if (IsStun())
        {
            return;
        }
        */
        if (controller.isGrounded && targetLane > MinLane)
        {
            Debug.Log("left:"+targetLane);
            targetLane--;
        }
    }

    public void MoveToRight()
    {
        if (IsStun())
        {
            return;
        }
        if (controller.isGrounded && targetLane < MaxLane)
        {
            Debug.Log("right:" + targetLane);
            targetLane++;
        }
    }

    public void Jump()
    {
        if (IsStun())
        {
            return;
        }
        if (controller.isGrounded)
        {
            Debug.Log("jump");
            moveDirection.y = speedJump;
            animator.SetTrigger("jump");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStun())
        {
            return;
        }

        if (hit.gameObject.tag == "Robo")
        {
            life--;
            recoverTime = StunDuration;

            animator.SetTrigger("damage");

            Destroy(hit.gameObject);
        }
    }
}
