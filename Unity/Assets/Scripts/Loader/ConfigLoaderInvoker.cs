using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    [Invoke]
    public class GetAllConfigBytes : AInvokeHandler<ConfigLoader.GetAllConfigBytes, ETTask<Dictionary<Type, byte[]>>>
    {
        public override async ETTask<Dictionary<Type, byte[]>> Handle(ConfigLoader.GetAllConfigBytes args)
        {
            Dictionary<Type, byte[]> output = new Dictionary<Type, byte[]>();
            HashSet<Type> configTypes = CodeTypes.Instance.GetTypes(typeof (ConfigAttribute));
            
            if (Define.IsEditor)
            {
                string ct = "c";
                foreach (Type configType in configTypes)
                {
                    string configFilePath;
                    configFilePath = $"../Config/Excel/{ct}/{configType.Name}.bytes";
                    output[configType] = File.ReadAllBytes(configFilePath);
                }
            }
            else
            {
                foreach (Type type in configTypes)
                {
                    TextAsset v = await ResourcesComponent.Instance.LoadAssetAsync<TextAsset>($"Assets/Bundles/Config/{type.Name}.bytes");
                    output[type] = v.bytes;
                }
            }

            return output;
        }
    }

    [Invoke]
    public class GetOneConfigBytes : AInvokeHandler<ConfigLoader.GetOneConfigBytes, ETTask<byte[]>>
    {
        public override async ETTask<byte[]> Handle(ConfigLoader.GetOneConfigBytes args)
        {
            string configName = args.ConfigName;
            byte[] result = null;
            if (Define.IsEditor)
            {
                string ct = "c";
                var configFilePath = $"../Config/Excel/{ct}/{configName}.bytes";
                result = File.ReadAllBytes(configFilePath);
            }
            else
            {
                TextAsset v = await ResourcesComponent.Instance.LoadAssetAsync<TextAsset>($"Assets/Bundles/Config/{configName}.bytes");
                result = v.bytes;
            }
            await ETTask.CompletedTask;
            return result;
        }
    }
}