using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection;

namespace SkyBlazor.Core
{
    /// <summary>
    /// 自动绑定元素的class、元素的id，JS自动注入
    /// </summary>
    public class SkyBlazorComponentBase : ComponentBase
	{
		[Inject]
		private IJSRuntime JS { get; set; } = default!;

		/// <summary>
		/// 当你需要调用注入的JS文件时请使用该接口
		/// </summary>
		protected IJSObjectReference? Module;

		/// <summary>
		/// 当你需要绑定class属性时请使用该接口
		/// </summary>
		protected virtual string ClassCssValue
		{
			set
			{
				ClassCssValue = value;
			}
			get
			{
				return ClassCSSProcessor.Default().AddFromAdditionalAttributes(AdditionalAttributes).Get();
			}
		}

		/// <summary>
		/// 当你需要绑定id属性时请使用该接口
		/// </summary>
		protected virtual string IdCssValue
		{
			set
			{
				IdCssValue = value;
			}
			get
			{
				return IdCSSProcessor.Default().AddFromAdditionalAttributes(AdditionalAttributes).Get();
			}
		}

		[Parameter(CaptureUnmatchedValues = true)]
		public IDictionary<string, object>? AdditionalAttributes { get; set; }

		/// <summary>
		/// 解析JSInject特性，导入指定的JS文件
		/// </summary>
		/// <returns>一个可用的JS调用接口</returns>
		/// <exception cref="AmbiguousMatchException"></exception>
		private async Task<IJSObjectReference?> JSQuickInject()
		{
			Type type = this.GetType();
			object[] customAttributes = type.GetCustomAttributes(typeof(JSInjectAttribute), true);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			else if (customAttributes.Length != 1)
			{
				throw new AmbiguousMatchException("每个razor组件只能存在一个JSInjectAttribute特性");
			}
			JSInjectAttribute jSInjectAttribute = (JSInjectAttribute)customAttributes[0];
			string jsFilePath = jSInjectAttribute.GetPath();
			try
			{
				return await JS.InvokeAsync<IJSObjectReference>("import", jsFilePath);
			}
			catch (Exception e)
			{
				throw new ArgumentException($"注入的js文件路径:{jsFilePath}有误！", e);
			}
		}

		/// <summary>
		/// 在这里绑定JS调用API
		/// </summary>
		/// <param name="firstRender"></param>
		/// <returns></returns>
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			Module = await JSQuickInject();
			await base.OnAfterRenderAsync(firstRender);
		}
	}
}