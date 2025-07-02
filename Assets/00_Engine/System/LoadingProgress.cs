using System;

namespace Engine
{
    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> ProgressChanged;

        private const float Ratio = 1f;
        
        public void Report(float value)
        {
            ProgressChanged?.Invoke(value / Ratio);
        }
    }
}
