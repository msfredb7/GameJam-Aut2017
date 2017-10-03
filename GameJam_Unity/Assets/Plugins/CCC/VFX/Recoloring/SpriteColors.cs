using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FullInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SpriteColors : MonoBehaviour
{
    const string MATERIALNAME = "Materials/ColorAdjustments";

    [Range(0, 1)]
    public float affectRangeMin = 0;
    [Range(0, 1)]
    public float affectRangeMax = 1;
    [Range(0, 1)]
    public float hueShift = 0;
    [Range(-1, 1)]
    public float saturation = 0;
    [Range(-1, 1)]
    public float value = 0;
    [Range(-1, 1)]
    public float alpha = 0;

    private MaterialPropertyBlock propertyBlock;
    private Renderer _renderer;

    void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();

        if (Application.isPlaying)
            return;

        Material material = Resources.Load(MATERIALNAME) as Material;

        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().sharedMaterial = material;

        Apply();
    }

    public void Verify()
    {
        if (affectRangeMax < affectRangeMin)
            affectRangeMax = affectRangeMin;
    }

    public void Apply()
    {
        Verify();

        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();

        _renderer.GetPropertyBlock(propertyBlock);

        if (sprRenderer.sprite == null)
            return;

        Material mat = sprRenderer.sharedMaterial;
        Texture texture =  sprRenderer.sprite.texture;

        propertyBlock.SetTexture("_MainTex", texture);
        propertyBlock.SetFloat("_HSVRangeMin", affectRangeMin);
        propertyBlock.SetFloat("_HSVRangeMax", affectRangeMax);
        propertyBlock.SetVector("_HSVAAdjust", new Vector4(hueShift, saturation, value, alpha));

        _renderer.SetPropertyBlock(propertyBlock);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(SpriteColors))]
public class ColorAdjustmentsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();

        if (EditorGUI.EndChangeCheck())
            (target as SpriteColors).Apply();
    }
}
#endif
