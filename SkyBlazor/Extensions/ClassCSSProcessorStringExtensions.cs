using SkyBlazor.Core;

namespace System
{
	/// <summary>
	/// 对string类型的扩展
	/// </summary>
	public static class ClassCSSProcessorStringExtensions
	{
		/// <summary>
		/// 解构一个css类字符串，获得一个ClassCSSProcessor对象
		/// </summary>
		/// <param name="cssString">一个css类字符串</param>
		/// <returns></returns>
		public static ClassCSSProcessor ReGet(this string cssString)
		{
			return ClassCSSProcessor.Default(cssString);
		}

		/// <summary>
		/// 解构一个css类字符串并追加一个css类字符串，获得一个ClassCSSProcessor对象
		/// </summary>
		/// <param name="cssString">需要给解构的字符串</param>
		/// <param name="cssValue">需要追加的字符串</param>
		/// <returns></returns>
		public static ClassCSSProcessor Add(this string cssString, string cssValue)
		{
			ClassCSSProcessor classCSSProcessor = cssString.ReGet();
			return classCSSProcessor.Add(cssValue);
		}
	}
}
