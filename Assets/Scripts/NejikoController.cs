using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;

    //charactorControllの時は、自分で重力を設定の必要がある。
    public float gravity;
    public float speedZ;
    public float speedJump;
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
        //キャラクターが地面に接地してたら
        if (controller.isGrounded)
        {
            //上を押すと+1、下を押すと-1
            if (Input.GetAxis("Vertical") > 0.0f)
            {
                moveDirection.z = Input.GetAxis("Vertical") * speedZ;
            }
            else
            {
                moveDirection.z = 0;
            }
            //キャラクターの回転
            //unityは左手系の座標だから右を押すと時計回り
            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = speedJump;
                animator.SetTrigger("jump");
            }
        }

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
}
