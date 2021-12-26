using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Start is called before the first frame update
    public bool isMove;
    Vector3 target;
    public float speed;
    Vector3[] path = null;
    private int index = -1;

    void Start() {

    }


    public void MoveTo(Vector3 pos) {
    }

    public void MoveToPath(Vector3[] pos) {
        Reset();
        path = pos;
        NextPos();

    }
    // Update is called once per frame
    void Update() {
        if (isMove) {
            //Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            var pos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            transform.position = pos;
            if (Vector3.Distance(pos, target) < 0.1f) {
                NextPos();
            }
        }
    }

    //public  Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta) {
    //    float num = target.x - current.x;
    //    float num2 = target.y - current.y;
    //    float num3 = target.z - current.z;
    //    float num4 = num * num + num2 * num2 + num3 * num3;
    //    if (num4 == 0f || (maxDistanceDelta >= 0f && num4 <= maxDistanceDelta * maxDistanceDelta)) {
    //        return target;
    //    }

    //    float num5 = (float)Math.Sqrt(num4);
    //    return new Vector3(current.x + num / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
    //}

    private void Reset() {
        isMove = false;
        index = -1;
        path = null;
    }

    private void NextPos() {
        index++;
        if (index >= path.Length) {
            Reset();
            return;
        }
        isMove = true;
        target = path[index];
    }

    private void Move2Angle() {

    }
}
