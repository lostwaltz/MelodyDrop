using System.Text;

namespace Engine
{
    public static class Util
    {
        public static readonly StringBuilder Str = new();
    }

    public static class ShortCut
    {
        public static T Get<T>() where T : class
        {
            return ServiceManager.Instance.Get<T>();
        }
    }
}
