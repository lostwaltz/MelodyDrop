using System.Collections.Generic;
using UnityEngine;


namespace Engine
{
    public class UIManager : SingletonMono<UIManager>
    {
        private readonly Dictionary<string, UIBase> _uiDic = new();

        public T GetUI<T>() where T : UIBase
        {
            var uiName = typeof(T).Name;

            if (IsExist<T>())
                return _uiDic[uiName] as T;
            
            return CreateUI<T>();
        }

        private T CreateUI<T>() where T : UIBase
        {
            var uiName = typeof(T).Name;

            var uiObj = Instantiate(Resources.Load<T>(uiName));
            
            RegisterUI(uiObj);

            return uiObj;
        }

        public void RegisterUI<T>(T ui) where T : UIBase
        {
            var uiName = typeof(T).Name;
            
            if (IsExist<T>())
                _uiDic[uiName] = ui;
            else
                _uiDic.Add(uiName, ui);
        }

        private bool IsExist<T>()
        {
            var uiName = typeof(T).Name;
            return _uiDic.ContainsKey(uiName) && _uiDic[uiName] != null;
        }

        public T OpenUI<T>() where T : UIBase
        {
            var ui = GetUI<T>();
            ui.Open();

            return ui;
        }

        public T CloseUI<T>() where T : UIBase
        {
            var ui = GetUI<T>();
            ui.Close();

            return ui;
        }

        public void DestroyUI<T>() where T : UIBase
        {
            var uiName = typeof(T).Name;

            if (!IsExist<T>()) return;
            
            var ui = _uiDic[uiName];

            Object.Destroy(ui.gameObject);

            _uiDic.Remove(uiName);
        }

        public void ReleaseUI<T>() where T : UIBase
        {
            _uiDic.Remove(typeof(T).Name);
        }

        private void Clear()
        {
            _uiDic.Clear();
        }
    }
}