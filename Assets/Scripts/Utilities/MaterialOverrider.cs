using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOverrider : MonoBehaviour
{
    public List<MaterialOverride> materials = new List<MaterialOverride>();

    private Renderer renderer;

    private void Awake()
    {
        UpdateMats();
    }


    private void OnValidate()
    {
        UpdateMats();
    }

    public void UpdateMats()
    {
        if (renderer == null)
            renderer = GetComponent<Renderer>();


        // update list
        var matCount = renderer.sharedMaterials.Length;
        if (matCount != materials.Count)
        {
            materials.Clear();
            for (int i = 0; i < matCount; i++)
            {
                materials.Add(new MaterialOverride
                {
                    block = new MaterialPropertyBlock(),
                    color = renderer.sharedMaterials[i].color,
                });
            }
        }


        // update colors
        for (int i = 0; i < materials.Count; i++)
        {
            var material = materials[i];
            var bloc = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(bloc, i);
            bloc.SetColor("_BaseColor", material.color);
            bloc.SetColor("_EmissionColor", material.emissionColor);
            bloc.SetFloat("_Smoothness", material.smoothness);
            if (material.baseMap != null)
            {
                bloc.SetTexture("_BaseMap", material.baseMap);
                bloc.SetFloat("_Scale", material.textureScale);
            }

            bloc.SetFloat("_OutlineWidth", material.outline);
            bloc.SetVector("_BaseMap_ST",
                new Vector4(material.scale.x, material.scale.y, material.offset.x, material.offset.y));

            material.block = bloc;
            renderer.SetPropertyBlock(bloc, i);
        }
    }

    internal void UpdateColor(Color color)
    {
        materials.ForEach(m => m.color = color);
        UpdateMats();
    }

    internal void UpdateColor(int materialIndex, Color color)
    {
        materials[materialIndex].color = color;
        UpdateMats();
    }
}

[Serializable]
public class MaterialOverride
{
    public MaterialPropertyBlock block;
    public Color color;
    public Color emissionColor;
    [Range(0f, 1f)] public float smoothness;
    public Texture baseMap;
    public float textureScale = 1;
    public float outline = 2;
    public Vector2 scale = Vector2.one;
    public Vector2 offset = Vector2.zero;
    public bool matchScaleWithTextureSize;
}