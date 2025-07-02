using System;
using Eflatun.SceneReference;
using JetBrains.Annotations;

namespace Engine
{
    [Serializable]
    public class SceneData
    {
        [CanBeNull] public SceneReference reference = null;
        public SceneType sceneType;
        
        public string Name => reference != null ? reference.Name : string.Empty;
    }
}