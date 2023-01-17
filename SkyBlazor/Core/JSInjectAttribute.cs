namespace SkyBlazor.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class JSInjectAttribute : Attribute
    {
        private string _path;

        /// <summary>
        /// js文件打包后所在的位置
        /// </summary>
        /// <param name="path"></param>
        public JSInjectAttribute(string path)
        {
            _path = path;
        }

        /// <summary>
        /// 获得js文件的路径
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return _path;
        }
    }
}
