using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class VImage : Image {

    [SerializeField]
    private SpriteAtlas spriteAtlas;
    public SpriteAtlas SpriteAtlas {
        get { return spriteAtlas; }
        set { spriteAtlas = value; }
    }

    private string lastSpriteName;
    [SerializeField]
    private string spriteName;
    public string SpriteName {
        get { return spriteName; }
        set {
            spriteName = value;
            SetAllDirty();
        }
    }

    protected VImage()
            : base() {
    }

    public override void SetMaterialDirty() {
        if (lastSpriteName != spriteName) {
            lastSpriteName = spriteName;
            sprite = spriteAtlas ? spriteAtlas.GetSprite(spriteName) : null;
        }

        base.SetMaterialDirty();
    }

    protected override void OnPopulateMesh(VertexHelper toFill) {
        if (!overrideSprite) {
            toFill.Clear();
            return;
        }
        base.OnPopulateMesh(toFill);
    }
}
