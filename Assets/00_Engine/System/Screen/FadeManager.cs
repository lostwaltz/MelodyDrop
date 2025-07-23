using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace Engine
{
    public class FadeManager : Singleton<FadeManager>
    {
        [SerializeField] private CanvasGroup fadeCanvas;

        private void Start()
        {
            FadeOut(0.1f).Forget();

        }

        public async UniTask FadeIn(float duration = 1f)
        {
            if (fadeCanvas == null) return;
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                fadeCanvas.alpha = Mathf.Lerp(0, 1, timer / duration);
                await UniTask.Yield();
            }

            fadeCanvas.alpha = 1;
        }

        public async UniTask FadeOut(float duration = 1f)
        {
            if (fadeCanvas == null) return;
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                fadeCanvas.alpha = Mathf.Lerp(1, 0, timer / duration);
                await UniTask.Yield();
            }

            fadeCanvas.alpha = 0;
        }
    }
}