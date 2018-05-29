namespace All4thlulz.Editor
{
    using UnityEngine;
    using UnityEditor;

    public class StarNestSkyboxOriginalInspector : StarNestInspectorBase
    {
        //Additional Properties

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            {
                //Find Properties
                //base.OnGUI(materialEditor, props);
                SkyboxOriginalMaterialVariables(materialEditor, props);
                CommonMaterialVariables(materialEditor, props);
            }

            Material material = materialEditor.target as Material;

            {
                //Shader Properties GUI
                EditorGUIUtility.labelWidth = 0f;

                EditorGUI.BeginChangeCheck();
                {
                    EditorGUILayout.HelpBox("Assign this material to the skybox in the Lighting settings or by dragging the material into the scene.", MessageType.None, true);
                    EditorGUILayout.Space();

                    AddPerformanceAndQualityCommonGUI(materialEditor, props, material);
                    EditorGUILayout.Space();
                    AddColorSettingsCommonGUI(materialEditor, props, material);
                    EditorGUILayout.Space();
                    AddFractalSettingsCommonGUI(materialEditor, props, material);
                    EditorGUILayout.Space();
                    AddGeneralSettingsSkyboxGUI(materialEditor, props, material);
                }
            }
            EditorGUI.EndChangeCheck();
        }
    }
}