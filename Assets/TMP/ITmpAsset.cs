using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITmpAsset {
    TMP_Settings GetTmpSetting();
    UnityEngine.Object GetAsset(string name);
}
