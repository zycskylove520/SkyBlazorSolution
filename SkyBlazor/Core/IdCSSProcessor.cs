namespace SkyBlazor.Core
{
	/// <summary>
	/// CSS ID预处理器
	/// </summary>
	public class IdCSSProcessor
	{
		private string _idValue;

		private IdCSSProcessor()
		{
			_idValue = "";
		}

		private IdCSSProcessor(string idValue)
		{
			this._idValue = idValue;
		}

		/// <summary>
		/// 静态构造一个IdCSSProcessor类
		/// </summary>
		/// <returns></returns>
		public static IdCSSProcessor Default()
		{
			return new IdCSSProcessor();
		}

		/// <summary>
		/// 静态构造一个IdCSSProcessor类
		/// </summary>
		/// <param name="idValue">设置id值</param>
		/// <returns></returns>
		public static IdCSSProcessor Default(string? idValue)
		{
			if (string.IsNullOrEmpty(idValue))
			{
				return new IdCSSProcessor();
			}
			return new IdCSSProcessor(idValue);
		}

		/// <summary>
		/// 修改已存在的id值
		/// </summary>
		/// <param name="idValue">新的id值</param>
		/// <returns></returns>
		public IdCSSProcessor Add(string? idValue)
		{
			if (string.IsNullOrEmpty(idValue))
			{
				return this;
			}
			_idValue = idValue;
			return this;
		}

		/// <summary>
		/// 往id属性中追加来自AdditionalAttributes的css属性
		/// </summary>
		/// <param name="additionalAttributes"></param>
		/// <returns></returns>
		public IdCSSProcessor AddFromAdditionalAttributes(IDictionary<string, object>? additionalAttributes)
		{
			if (additionalAttributes == null)
			{
				return this;
			}
			if (additionalAttributes.TryGetValue("id", out var value))
			{
				Add(value as string);
				return this;
			}
			return this;
		}

		/// <summary>
		/// 移除已存在的id值
		/// </summary>
		/// <returns></returns>
		public IdCSSProcessor Remove()
		{
			_idValue = "";
			return this;
		}

		/// <summary>
		/// 获取id值
		/// </summary>
		/// <returns></returns>
		public string Get()
		{
			return _idValue;
		}
	}
}
