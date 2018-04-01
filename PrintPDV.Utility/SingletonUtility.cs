

namespace PrintPDV.Utility
{
    public class SingletonUtility<T> where T : class, new()
    {
        private SingletonUtility() { }

        internal class SingletonCreator
        {
            static SingletonCreator() { }

            internal static readonly T instance = new T();
        }

        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }
    }
}
