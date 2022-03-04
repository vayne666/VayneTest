using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class VImage : Image {

    private bool isChanged;
    [SerializeField]
    private SpriteAtlas spriteAtlas;

    public SpriteAtlas SpriteAtlas {
        get { return spriteAtlas; }
        set {
            spriteAtlas = value;
            isChanged = true;
            SetAllDirty();
        }
    }

    private string lastSpriteName;
    [SerializeField]
    private string spriteName;
    public string SpriteName {
        get { return spriteName; }
        set {
            spriteName = value;
            if (value != spriteName) isChanged = true;
            SetAllDirty();
        }
    }

    protected VImage() : base() {
    }

    public override void SetMaterialDirty() {
        if (lastSpriteName != spriteName) SetSprite();
        base.SetMaterialDirty();
    }

    private void SetSprite() {
        if (string.IsNullOrEmpty(spriteName)) {
            sprite = null;
        } else {
            sprite = spriteAtlas ? spriteAtlas.GetSprite(spriteName) : null;
        }
        lastSpriteName = spriteName;
    }

    protected override void OnPopulateMesh(VertexHelper toFill) {
        if (!overrideSprite) {
            toFill.Clear();
            return;
        }
        base.OnPopulateMesh(toFill);
    }
}
