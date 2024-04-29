using UnityEngine;
using YooAsset;

namespace ET
{
    public enum BuildType
    {
        Debug,
        Release,
    }
    
    [CreateAssetMenu(menuName = "ET/CreateGlobalConfig", fileName = "GlobalConfig", order = 0)]
    public class GlobalConfig: ScriptableObject
    {
        public bool EnableDll;

        public BuildType BuildType;

        public AppType AppType;

        public EPlayMode EPlayMode;
    }
}