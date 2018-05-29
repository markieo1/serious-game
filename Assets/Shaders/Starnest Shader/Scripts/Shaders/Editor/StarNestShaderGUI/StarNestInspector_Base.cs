using System;

namespace All4thlulz.Editor
{
    using UnityEngine;
    using UnityEditor;

    /// <summary>
    /// Base properties for the StarNest shader gui that can be found on all variants
    /// </summary>
    public class StarNestInspectorBase : ShaderGUI
    {
        protected static MaterialProperty blendMode;

        protected static MaterialProperty iterations;
        protected static MaterialProperty volSteps;
        protected static MaterialProperty zoom;
        protected static MaterialProperty starNestMainColor;
        protected static MaterialProperty clampOut;
        protected static MaterialProperty centrePos;
        protected static MaterialProperty cameraScroll;
        protected static MaterialProperty hueShift;
        protected static MaterialProperty postSaturation;
        protected static MaterialProperty scroll;
        protected static MaterialProperty enable3DRotation;
        protected static MaterialProperty rotation;
        protected static MaterialProperty fractal;
        protected static MaterialProperty stepSize;
        protected static MaterialProperty tile;
        protected static MaterialProperty brightness;
        protected static MaterialProperty darkmatter;
        protected static MaterialProperty darkmatterContrast;
        protected static MaterialProperty distFading;
        protected static MaterialProperty saturation;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {

        }

        /// <summary>
        /// Used for render mode option.
        /// </summary>
        /// <param name="materialEditor">Pass the MaterialEditor from the GUI.</param>
        /// <param name="props">Pass the MaterialProperty array from the GUI.</param>
        public void RenderModeMaterialVariables(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            {
                //Find Properties
                blendMode = FindProperty("_Mode", props);
            }
        }

        /// <summary>
        /// Common starnest variables that are available on all starnest variants.
        /// </summary>
        /// <param name="materialEditor">Pass the MaterialEditor from the GUI.</param>
        /// <param name="props">Pass the MaterialProperty array from the GUI.</param>
        public void CommonMaterialVariables(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            {
                //Find Properties
                iterations = FindProperty("_Iterations", props);
                volSteps = FindProperty("_Volsteps", props);
                zoom = FindProperty("_Zoom", props);
                starNestMainColor = FindProperty("_StarNestMainColor", props);
                clampOut = FindProperty("_CLAMPOUT", props);
                hueShift = FindProperty("_HueShift", props);
                postSaturation = FindProperty("_PostSaturation", props);
                centrePos = FindProperty("_Center", props);
                scroll = FindProperty("_Scroll", props);
                rotation = FindProperty("_Rotation", props);
                fractal = FindProperty("_Formuparam", props);
                stepSize = FindProperty("_StepSize", props);
                tile = FindProperty("_Tile", props);
                brightness = FindProperty("_Brightness", props);
                darkmatter = FindProperty("_Darkmatter", props);
                darkmatterContrast = FindProperty("_DarkmatterContrastMultiplier", props);
                distFading = FindProperty("_Distfading", props);
                saturation = FindProperty("_Saturation", props);
            }
        }

        /// <summary>
        /// Starnest 2D variables that are available in these variants.
        /// </summary>
        /// <param name="materialEditor">Pass the MaterialEditor from the GUI.</param>
        /// <param name="props">Pass the MaterialProperty array from the GUI.</param>
        public void StarNest2DMaterialVariables(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            {
                //Find Properties
                enable3DRotation = FindProperty("_3DRotation", props);
            }
        }

        /// <summary>
        /// Skybox Original specific variables.
        /// This is here because I only just found the new variables and need a quick working shader GUI
        /// </summary>
        /// <param name="materialEditor">Pass the MaterialEditor from the GUI.</param>
        /// <param name="props">Pass the MaterialProperty array from the GUI.</param>
        public void SkyboxOriginalMaterialVariables(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            {
                //Find Properties
                cameraScroll = FindProperty("_CamScroll", props);
            }
        }

        #region Common




        /// <summary>
        ///
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="props"></param>
        /// <param name="material"></param>
        public void AddPerformanceAndQualityCommonGUI(MaterialEditor materialEditor, MaterialProperty[] props, Material material)
        {
            EditorGUILayout.HelpBox("Perfromance and Quality:", MessageType.None, true);
            materialEditor.ShaderProperty(volSteps, new GUIContent("Volumetric Steps", "Volumetric rendering steps. Each 'step' renders more objects at all distances. \nThis has a higher performance hit than iterations."), 0);
            if (material != null && material.GetFloat(volSteps.name) > 17)
            {
                EditorGUILayout.HelpBox("High volumetric steps could result in bad performace.", MessageType.Warning, true);
            }
            materialEditor.ShaderProperty(iterations, new GUIContent("Iterations", "Iterations of inner fractal loop. \nThe higher this is, the more distant objects get rendered."), 0);
            if (material != null && material.GetFloat(iterations.name) > 15)
            {
                EditorGUILayout.HelpBox("High iterations could result in bad performace. \nTry using Darkmatter Contrast instead.", MessageType.Warning, true);
            }

            materialEditor.ShaderProperty(stepSize, new GUIContent("Step Size", "How much farther each Volumetric Step goes."), 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="props"></param>
        /// <param name="material"></param>
        public void AddColorSettingsCommonGUI(MaterialEditor materialEditor, MaterialProperty[] props, Material material)
        {
            EditorGUILayout.HelpBox("Color Settings:", MessageType.None, true);
            materialEditor.ShaderProperty(starNestMainColor, new GUIContent("Main Color", "Tint the color of the result with this."));
            materialEditor.ShaderProperty(clampOut, new GUIContent("Clamp Color", "Clamp output with main color as max"));
            materialEditor.ShaderProperty(brightness, new GUIContent("Brightness", "Brightness scale."), 0);
            materialEditor.ShaderProperty(saturation, new GUIContent("Quick Saturation", "How much color is present."), 0);
            materialEditor.ShaderProperty(hueShift, new GUIContent("Hue Shift", "Shift in hue."), 0);
            if (material != null && material.GetFloat(hueShift.name) != 0)
            {
                materialEditor.ShaderProperty(postSaturation, new GUIContent("Saturation Shift", "Post-Adjust in saturation."), 0);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="props"></param>
        /// <param name="material"></param>
        public void AddFractalSettingsCommonGUI(MaterialEditor materialEditor, MaterialProperty[] props, Material material)
        {
            EditorGUILayout.HelpBox("Fractal Settings:", MessageType.None, true);
            materialEditor.ShaderProperty(fractal, new GUIContent("Fractal Magic", "Controls the fractal."), 0);
            materialEditor.ShaderProperty(tile, new GUIContent("Tile", "Fractal repeating rate. \nLow numbers are busy and give lots of repitition. \nHigh numbers are very sparce"), 0);
            materialEditor.ShaderProperty(zoom, new GUIContent("Zoom", "Zoom level for the repeating fractal."), 0);
            materialEditor.ShaderProperty(darkmatter, new GUIContent("Darkmatter", "Abundance of Dark matter (in the distance).\nVisible with Volsteps higher than 7."), 0);
            materialEditor.ShaderProperty(darkmatterContrast, new GUIContent("Darkmatter Contrast", "Multiplies the brightness of darkmatter. \nUseful to help performance by lower Iterations and increasing this contrast"), 0);
            materialEditor.ShaderProperty(distFading, new GUIContent("Distance Fading", "Brightness of distant objects. \nMay affect brightness of darkmatter."), 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="props"></param>
        /// <param name="material"></param>
        public void AddGeneralSettingsCommonGUI(MaterialEditor materialEditor, MaterialProperty[] props, Material material)
        {
            EditorGUILayout.HelpBox("General Settings:", MessageType.None, true);
            materialEditor.ShaderProperty(scroll, new GUIContent("Scroll Direction", "x,y,z - Scrolls in this direction over time. \n w for speed multiplier."), 0);
            materialEditor.ShaderProperty(enable3DRotation, new GUIContent("Enable 3D Rotation", "Enabled: Rotates based on view direction in world space. \nDisabled: Rotates the stars."), 0);
            materialEditor.ShaderProperty(rotation, new GUIContent("Rotation", " x,y,z - for axis to rotate on. \n w to multiply the rotation angle."), 0);
            materialEditor.ShaderProperty(centrePos, new GUIContent("Centre", "Center position in space and time. \nx,y - for changing the starting position of the fractal. \nw - for time offset."), 0);
        }
        #endregion

        #region Skybox Specific
        /// <summary>
        /// Ommits the rotation variable for the original version of the skybox that does not need it
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="props"></param>
        /// <param name="material"></param>
        public void AddGeneralSettingsSkyboxGUI(MaterialEditor materialEditor, MaterialProperty[] props, Material material)
        {
            EditorGUILayout.HelpBox("General Settings:", MessageType.None, true);
            materialEditor.ShaderProperty(cameraScroll, new GUIContent("Camera Scroll", "How much does camera position cause the fractal to scroll. \nCan give a really cool depth effect as the player camera moves."), 0);
            if (material != null && Math.Abs(material.GetFloat(cameraScroll.name)) > 0.01)
            {
                EditorGUILayout.HelpBox("Move the Main Camera in Unity while in Play mode to see how this affects the depth in the Game scene.", MessageType.Info, true);
            }
            materialEditor.ShaderProperty(scroll, new GUIContent("Scroll Direction", "x,y,z - Scrolls in this direction over time. \n w for speed multiplier."), 0);
            materialEditor.ShaderProperty(rotation, new GUIContent("Rotation", " x,y,z - for axis to rotate on. \n w to multiply the rotation angle."), 0);
            materialEditor.ShaderProperty(centrePos, new GUIContent("Centre", "Center position in space and time. \nx,y - for changing the starting position of the fractal. \nw - for time offset."), 0);
        }
        #endregion
    }
}