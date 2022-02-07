using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    Vector3 diff;

    public GameObject target;
    public float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        diff = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //LateUpdateが処理される度に距離が縮まりその距離の割合で決まるので
        //最初の移動距離は長く段々移動距離が短くなる。最初はダッシュ、止まる直前はピタっと止まるのではなく
        //段々止まる感じになる。
        transform.position = Vector3.Lerp(
            //A地点
            transform.position,
            //B地点
            target.transform.position - diff,
            //A-B地点の間の割合
            Time.deltaTime * followSpeed
            );
    }
}
