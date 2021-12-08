using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
public class AntiCheatToolkitTest : MonoBehaviour {

    private int num = 0;
    public Button btn;
    public Text text;




    private ObscuredInt num1 = 0;
    public Button btn1;
    public Text text1;


    void Start() {
        btn.onClick.AddListener(() => {
            num++;
            text.text=num.ToString();
        });

        btn1.onClick.AddListener(() => {
            num1++;
            text1.text=num1.ToString();
        });
        UnityEditor.Sprites.Packer.RebuildAtlasCacheIfNeeded(UnityEditor.BuildTarget.Android);
        UnityEditor.U2D.SpriteAtlasUtility.PackAllAtlases(UnityEditor.EditorUserBuildSettings.activeBuildTarget);
    }

    // Update is called once per frame
    void Update() {

    }
}
