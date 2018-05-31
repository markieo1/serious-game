

namespace All4thlulz.Editor
{
    using System;
    using UnityEditor;
    using UnityEngine;

    public class ShaderGUIBlendMode
    {
        public enum BlendModeAll
        {
            Opaque = 0,
            Cutout,
            Fade, // Old school alpha-blending mode, fresnel does not affect amount of transparency
            Transparent, // Physically plausible transparency mode, implemented as alpha pre-multiply
            Skybox
        }

        public enum BlendModeStandard
        {
            Opaque = 0,
            Cutout,
            Fade, // Old school alpha-blending mode, fresnel does not affect amount of transparency
            Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
        }

        private static void SetupMaterialWithBlendModeAll(Material material)
        {
            switch ((BlendModeAll) material.GetFloat("_Mode"))
            {
                case BlendModeAll.Opaque:
                    material.SetOverrideTag("RenderType", "");
                    material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;
                case BlendModeAll.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.AlphaTest;
                    break;
                case BlendModeAll.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendModeAll.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendModeAll.Skybox:
                    material.SetOverrideTag("RenderType", "Background");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 0);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Background;
                    break;
                default:
                    throw new NotImplementedException("This BlendMode does not exist. Try changing Render Mode or Reset the shader to default.");
            }
        }

        private static void SetupMaterialWithBlendModeStandard(Material material)
        {
            switch ((BlendModeStandard)material.GetFloat("_Mode"))
            {
                case BlendModeStandard.Opaque:
                    material.SetOverrideTag("RenderType", "");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;
                case BlendModeStandard.Cutout:
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    break;
                case BlendModeStandard.Fade:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                case BlendModeStandard.Transparent:
                    material.SetOverrideTag("RenderType", "Transparent");
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    break;
                default:
                    throw new NotImplementedException("This BlendMode does not exist. Try changing Render Mode or Reset the shader to default.");
            }
        }

        /// <summary>
        /// Will add the blend mode drop down to the shader material gui.
        /// Will list all available render modes in the gui.
        /// Shader should contain: _SrcBlend, _DstBlend, _ZWrite, _ALPHATEST_ON, _ALPHABLEND_ON, _ALPHAPREMULTIPLY_ON.
        ///
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="property"></param>
        /// <param name="material"></param>
        public static void AddBlendModeAllToShaderGUI(MaterialEditor materialEditor, MaterialProperty property, Material material)
        {
            EditorGUILayout.HelpBox("Render Mode: \n(Opaque is the only render modes fully supported right now.)", MessageType.None, true);

            EditorGUI.showMixedValue = property.hasMixedValue;
            var bMode = (BlendModeAll)property.floatValue;

            EditorGUI.BeginChangeCheck();
            bMode = (BlendModeAll)EditorGUILayout.Popup("Rendering Mode", (int)bMode, Enum.GetNames(typeof(BlendModeAll)));

            if (EditorGUI.EndChangeCheck())
            {
                materialEditor.RegisterPropertyChangeUndo("Rendering Mode");
                property.floatValue = (float)bMode;

                foreach (var obj in property.targets)
                {
                    SetupMaterialWithBlendModeAll((Material)obj);
                }
            }
            EditorGUI.showMixedValue = false;
        }

        /// <summary>
        /// Will add the blend mode drop down to the shader material gui.
        /// Will list all available render modes in the gui.
        /// Shader should contain: _SrcBlend, _DstBlend, _ZWrite, _ALPHATEST_ON, _ALPHABLEND_ON, _ALPHAPREMULTIPLY_ON.
        /// 
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="property"></param>
        /// <param name="material"></param>
        public static void AddBlendModeStandardToShaderGUI(MaterialEditor materialEditor, MaterialProperty property, Material material)
        {
            EditorGUILayout.HelpBox("Render Mode: \n(Opaque is the only render mode fully supported right now.)", MessageType.None, true);

            EditorGUI.showMixedValue = property.hasMixedValue;
            var bMode = (BlendModeStandard)property.floatValue;

            EditorGUI.BeginChangeCheck();
            bMode = (BlendModeStandard)EditorGUILayout.Popup("Rendering Mode", (int)bMode, Enum.GetNames(typeof(BlendModeStandard)));

            if (EditorGUI.EndChangeCheck())
            {
                materialEditor.RegisterPropertyChangeUndo("Rendering Mode");
                property.floatValue = (float)bMode;

                foreach (var obj in property.targets)
                {
                    SetupMaterialWithBlendModeAll((Material)obj);
                }
            }
            EditorGUI.showMixedValue = false;
        }
    }
}