﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VImage : MaskableGraphic {
    protected VImage() { useLegacyMeshGeneration=false; }

    protected override void OnPopulateMesh(VertexHelper toFill) { toFill.Clear(); }
}
