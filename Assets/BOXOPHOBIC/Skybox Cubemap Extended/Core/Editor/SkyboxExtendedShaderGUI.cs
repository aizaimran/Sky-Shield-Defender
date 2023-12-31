﻿//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SkyboxExtendedShaderGUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        //base.OnGUI(materialEditor, props);

        var material0 = materialEditor.target as Material;
        
        DrawDynamicInspector(material0, materialEditor, props);
    }

    void DrawDynamicInspector(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
    {
        var customPropsList = new List<MaterialProperty>();

        for (int i = 0; i < props.Length; i++)
        {
            var prop = props[i];

            if (prop.flags == MaterialProperty.PropFlags.HideInInspector)
                continue;

            customPropsList.Add(prop);
        }

        //Draw Custom GUI
        for (int i = 0; i < customPropsList.Count; i++)
        {
            var prop = customPropsList[i];

            if (prop.type == MaterialProperty.PropType.Texture)
            {
                EditorGUI.BeginChangeCheck();

                EditorGUI.showMixedValue = prop.hasMixedValue;

                Texture tex = null;

                if (prop.textureDimension == UnityEngine.Rendering.TextureDimension.Tex2D)
                {
                    tex = (Texture2D)EditorGUILayout.ObjectField(prop.displayName, prop.textureValue, typeof(Texture2D), false, GUILayout.Height(50));
                }

                if (prop.textureDimension == UnityEngine.Rendering.TextureDimension.Cube)
                {
                    tex = (Cubemap)EditorGUILayout.ObjectField(prop.displayName, prop.textureValue, typeof(Cubemap), false, GUILayout.Height(50));
                }

                EditorGUI.showMixedValue = false;

                if (EditorGUI.EndChangeCheck())
                {
                    prop.textureValue = tex;
                }
            }
            else
            {
                materialEditor.ShaderProperty(customPropsList[i], customPropsList[i].displayName);
            }
        }

        GUILayout.Space(10);
    }
}
