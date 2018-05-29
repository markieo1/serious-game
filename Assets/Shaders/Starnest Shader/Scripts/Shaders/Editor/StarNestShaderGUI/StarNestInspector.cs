namespace All4thlulz.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class StarNestInspector : StarNestInspectorBase
    {
        //Additional Properties

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            {
                //Find Properties
                //base.OnGUI(materialEditor, props);
                RenderModeMaterialVariables(materialEditor, props);
                StarNest2DMaterialVariables(materialEditor, props);
                CommonMaterialVariables(materialEditor, props);
            }

            Material material = materialEditor.target as Material;

            {
                //Shader Properties GUI
                EditorGUIUtility.labelWidth = 0f;

                EditorGUI.BeginChangeCheck();
                {
                    ShaderGUIBlendMode.AddBlendModeStandardToShaderGUI(materialEditor, blendMode, material);
                    EditorGUILayout.Space();

                    //TODO: support other rendering modes...
                    // If cutout we can show these values to edit
                    /*
                    // cutout blend mode
                    materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture", "Main Color Texture (RGB)"),
                        mainTexture, color);
                    EditorGUI.indentLevel += 2;
                    if ((BlendModeAll) material.GetFloat("_Mode") == BlendModeAll.Cutout)
                    {
                        materialEditor.ShaderProperty(alphaCutoff, "Alpha Cutoff", 2);
                    }*/

                    AddPerformanceAndQualityCommonGUI(materialEditor, props, material);
                    EditorGUILayout.Space();
                    AddColorSettingsCommonGUI(materialEditor, props, material);
                    EditorGUILayout.Space();
                    AddFractalSettingsCommonGUI(materialEditor, props, material);
                    EditorGUILayout.Space();
                    AddGeneralSettingsCommonGUI(materialEditor, props, material);
                }
            }
            EditorGUI.EndChangeCheck();
        }
    }
}