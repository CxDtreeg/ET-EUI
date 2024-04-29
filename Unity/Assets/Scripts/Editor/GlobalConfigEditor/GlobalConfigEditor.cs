using System;
using UnityEditor;

namespace ET
{
    [CustomEditor(typeof(GlobalConfig))]
    public class GlobalConfigEditor : Editor
    {
        private BuildType buildType;

        private void OnEnable()
        {
            GlobalConfig globalConfig = (GlobalConfig)this.target;
            globalConfig.BuildType = EditorUserBuildSettings.development ? BuildType.Debug : BuildType.Release;
            this.buildType = globalConfig.BuildType;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GlobalConfig globalConfig = (GlobalConfig)this.target;

            if (this.buildType != globalConfig.BuildType)
            {
                this.buildType = globalConfig.BuildType;
                EditorUserBuildSettings.development = this.buildType switch
                {
                    BuildType.Debug => true,
                    BuildType.Release => false,
                    _ => throw new ArgumentOutOfRangeException()
                };
                this.serializedObject.Update();
                AssemblyTool.DoCompile();
            }
        }
    }
}