using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


class TaskTest : MonoBehaviour {

    private void Start() {
        Test();
    }

    public async void Test() {
        await VTask.Delay(1);
        Debug.Log("1s");
        await VTask.Delay(2);
        Debug.Log("2s");
        Debug.Log("2s1");
    }
}

