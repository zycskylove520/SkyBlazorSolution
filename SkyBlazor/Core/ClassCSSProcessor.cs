namespace SkyBlazor.Core
{
	/// <summary>
	/// CSS Class预处理器
	/// </summary>
	public class ClassCSSProcessor
	{
		private readonly Dictionary<string, string> _classCssState;

		private ClassCSSProcessor(string cssValue): this()
		{
			string[]? parseCssArray = ParseString(cssValue);
			if (parseCssArray != null)
			{
				foreach (string singleCssValue in parseCssArray)
				{
					_classCssState.TryAdd(singleCssValue, singleCssValue);
				}
			}
		}

		private ClassCSSProcessor()
		{
			_classCssState = new Dictionary<string, string>();
		}

		/// <summary>
		/// 静态构造一个ClassCSSProcessor类
		/// </summary>
		/// <returns></returns>
		public static ClassCSSProcessor Default()
		{
			return new ClassCSSProcessor();
		}

		/// <summary>
		/// 静态构造一个ClassCSSProcessor类
		/// </summary>
		/// <param name="cssValue">一个css类的字符串</param>
		/// <returns></returns>
		public static ClassCSSProcessor Default(string? cssValue)
		{
			if (string.IsNullOrEmpty(cssValue))
			{
				return new ClassCSSProcessor();
			}
			return new ClassCSSProcessor(cssValue);
		}

		/// <summary>
		/// 静态构造一个ClassCSSProcessor类
		/// </summary>
		/// <param name="cssValueArray">多个css类组成的数组，支持混合</param>
		/// <returns></returns>
		public static ClassCSSProcessor Default(string[] cssValueArray)
		{
			ClassCSSProcessor classCSSProcessor = new ClassCSSProcessor();
			foreach (string cssValue in cssValueArray)
			{
				string[]? parseCssArray = ParseString(cssValue);
				if (parseCssArray != null)
				{
					foreach (string singleCssValue in parseCssArray)
					{
						classCSSProcessor._classCssState.TryAdd(singleCssValue, singleCssValue);
					}
				}
			}
			return classCSSProcessor;
		}

		/// <summary>
		/// 静态构造一个ClassCSSProcessor类
		/// </summary>
		/// <param name="cssValueCollection">多个css类组成的可迭代集合，支持混合</param>
		/// <returns></returns>
		public static ClassCSSProcessor Default(IEnumerable<string> cssValueCollection)
		{
			ClassCSSProcessor classCSSProcessor = new ClassCSSProcessor();
			foreach (string cssValue in cssValueCollection)
			{
				foreach (string singleCssValue in ClassCSSProcessor.ParseString(cssValue))
				{
					classCSSProcessor._classCssState.TryAdd(singleCssValue, singleCssValue);
				}
			}
			return classCSSProcessor;
		}

		/// <summary>
		/// 静态构造一个ClassCSSProcessor类
		/// </summary>
		/// <param name="classCSSProcessor">解析一个ClassCSSProcessor来构造</param>
		/// <returns></returns>
		public static ClassCSSProcessor Default(ref ClassCSSProcessor classCSSProcessor)
		{
			string cssValue = classCSSProcessor.Get();
			return new ClassCSSProcessor(cssValue);
		}

		/// <summary>
		/// 往class属性中追加css属性
		/// </summary>
		/// <param name="cssValue">一个css类的字符串</param>
		/// <returns></returns>
		public ClassCSSProcessor Add(string? cssValue)
		{
			if(String.IsNullOrEmpty(cssValue))
			{
				return this;
			}
			string[]? parseCssArray = ParseString(cssValue);
			if(parseCssArray != null)
			{
				foreach (string singleCssValue in parseCssArray)
				{
					_classCssState.TryAdd(singleCssValue, singleCssValue);
				}
			}
			return this;
		}

		/// <summary>
		/// 往class属性中追加css属性
		/// </summary>
		/// <param name="cssValueArray">多个css类组成的数组，支持混合</param>
		/// <returns></returns>
		public ClassCSSProcessor Add(string[] cssValueArray)
		{
			foreach (string cssValue in cssValueArray)
			{
				string[]? parseCssArray = ParseString(cssValue);
				if (parseCssArray != null)
				{
					foreach (string singleCssValue in parseCssArray)
					{
						_classCssState.TryAdd(singleCssValue, singleCssValue);
					}
				}
			}
			return this;
		}

		/// <summary>
		/// 往class属性中追加css属性
		/// </summary>
		/// <param name="cssValueCollection">多个css类组成的可迭代集合，支持混合</param>
		/// <returns></returns>
		public ClassCSSProcessor Add(IEnumerable<string> cssValueCollection)
		{
			foreach (string cssValue in cssValueCollection)
			{
				string[]? parseCssArray = ParseString(cssValue);
				if (parseCssArray != null)
				{
					foreach (string singleCssValue in parseCssArray)
					{
						_classCssState.TryAdd(singleCssValue, singleCssValue);
					}
				}
			}
			return this;
		}

		/// <summary>
		/// 往class属性中追加css属性
		/// </summary>
		/// <param name="classCSSProcessor">解析一个已存在的ClassCSSProcessor继续拼接</param>
		/// <returns></returns>
		public ClassCSSProcessor Add(ClassCSSProcessor classCSSProcessor)
		{
			string cssValue = classCSSProcessor.Get();
			return Add(cssValue);
		}

		/// <summary>
		/// 往class属性中追加来自AdditionalAttributes的css属性
		/// </summary>
		/// <param name="additionalAttributes"></param>
		/// <returns></returns>
		public ClassCSSProcessor AddFromAdditionalAttributes(IDictionary<string, object>? additionalAttributes)
		{
			if (additionalAttributes is null)
			{
				return this;
			}
			if (additionalAttributes.TryGetValue("class", out var value))
			{
				Add(value as string);
				return this;
			}
			return this;
		}

		/// <summary>
		/// 移除css属性值
		/// </summary>
		/// <param name="cssValue">一个css属性值</param>
		/// <returns></returns>
		public ClassCSSProcessor Remove(string? cssValue)
		{
			if (cssValue is null)
			{
				return this;
			}
			string[]? parseCssArray = ParseString(cssValue);
			if (parseCssArray != null)
			{
				foreach (string singleCssValue in parseCssArray)
				{
					_classCssState.Remove(singleCssValue);
				}
			}
			return this;
		}

		/// <summary>
		/// 移除css属性值
		/// </summary>
		/// <param name="cssValueArray">一个css属性值数组</param>
		/// <returns></returns>
		public ClassCSSProcessor Remove(string[] cssValueArray)
		{
			foreach (string cssValue in cssValueArray)
			{
				string[]? parseCssArray = ParseString(cssValue);
				if (parseCssArray != null)
				{
					foreach (string singleCssValue in parseCssArray)
					{
						_classCssState.Remove(singleCssValue);
					}
				}
			}
			return this;
		}

		/// <summary>
		/// 移除css属性值
		/// </summary>
		/// <param name="cssValueCollection">一个css属性值集合</param>
		/// <returns></returns>
		public ClassCSSProcessor Remove(IEnumerable<string> cssValueCollection)
		{
			foreach (string cssValue in cssValueCollection)
			{
				string[]? parseCssArray = ParseString(cssValue);
				if (parseCssArray != null)
				{
					foreach (string singleCssValue in parseCssArray)
					{
						_classCssState.Remove(singleCssValue);
					}
				}
			}
			return this;
		}

		/// <summary>
		/// 移除css属性值
		/// </summary>
		/// <param name="classCSSProcessor">解析一个已存在的ClassCSSProcessor来移除class属性</param>
		/// <returns></returns>
		public ClassCSSProcessor Remove(ClassCSSProcessor classCSSProcessor)
		{
			string cssValue = classCSSProcessor.Get();
			return Remove(cssValue);
		}

		/// <summary>
		/// 清空已存在的所有css值
		/// </summary>
		/// <returns></returns>
		public ClassCSSProcessor RemoveCurrentAll()
		{
			_classCssState.Clear();
			return this;
		}

		/// <summary>
		/// 从一个包含多个css类的字符串中解析成css类数组
		/// </summary>
		/// <param name="multiCssValue"></param>
		/// <returns></returns>
		private static string[]? ParseString(string multiCssValue)
		{
			if (string.IsNullOrEmpty(multiCssValue))
			{
				return null;
			}
			string standardMultiCssValue = multiCssValue.Trim();
			string[] cssValueArray = standardMultiCssValue.Split(' ');
			return cssValueArray;
		}

		/// <summary>
		/// 获得最终的class属性字符串
		/// </summary>
		/// <returns></returns>
		public string Get()
		{
			if (_classCssState.Count() == 0)
			{
				return "";
			}
			return String.Join(' ', _classCssState.Values);
		}
	}
}
