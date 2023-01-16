using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace SkyBlazor.Component
{
	/// <summary>
	/// 移入弹窗组件，当鼠标进入移入框时会自动弹出弹出框
	/// </summary>
	public partial class MoveInPopover
	{
		[Inject]
		private IJSRuntime JS { get; set; } = default!;

		private IJSObjectReference? module;

		/// <summary>
		/// 当前弹出框状态是否弹出
		/// </summary>
		private bool _isPopState = false;

		/// <summary>
		/// 设置弹出框是否自动弹出，如果设置为false，则切换为鼠标点击触发弹出框弹出
		/// 一旦设置为鼠标点击弹出，请勿再自定义鼠标点击事件
		/// </summary>
		[Parameter]
		public bool IsAutoPopUp { get; set; }
		private bool _isAutoPopUp = true;

		/// <summary>
		/// 四个可选弹出方向
		/// </summary>
		public enum Direction
		{
			/// <summary>
			/// 弹出方向为向左弹出
			/// </summary>
			Left,

			/// <summary>
			/// 弹出方向为向右弹出
			/// </summary>
			Right,

			/// <summary>
			/// 弹出方向为向上弹出
			/// </summary>
			Top,

			/// <summary>
			/// 弹出方向为向下弹出
			/// </summary>
			Bottom
		}

		/// <summary>
		/// 设置弹窗的弹出方向，如果弹出方向溢出会自动更换合适的弹出方向
		/// 自动更换合适的弹出方向只适用于向左或向右弹出
		/// </summary>
		[Parameter]
		public Direction PopDirection { get; set; } = Direction.Bottom;

		/// <summary>
		/// 设置移入框部件，不设置则使用默认移入框
		/// </summary>
		[Parameter]
		public RenderFragment? MoveInContent { get; set; }

		/// <summary>
		/// 设置弹出框部件
		/// </summary>
		[Parameter]
		[NotNull]
		public RenderFragment? PopoverContent { get; set; }

		/// <summary>
		///  为弹出框添加额外添加额外自定义属性
		/// </summary>
		[Parameter(CaptureUnmatchedValues = true)]
		public IDictionary<string, object>? AdditionalAttributes { get; set; }

		/// <summary>
		/// 为移入框设置的点击事件
		/// 一旦设置了自定义的点击事件，那么弹出框的弹出方式会自动变为移入弹出
		/// </summary>
		[Parameter]
		public EventCallback<MouseEventArgs> MouseClickEvent { get; set; }
		private EventCallback<MouseEventArgs> _mouseClickEvent;

		/// <summary>
		/// 鼠标进入移入框时触发事件
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NullReferenceException"></exception>
		private async Task MouseMoveIn()
		{
			if (_isAutoPopUp)
			{
				if (module != null)
				{
					await module.InvokeVoidAsync("AutoPopoverLocateShow", PopDirection);
					_isPopState = true;
				}
				else
				{
					throw new NullReferenceException();
				}
			}
			else
			{
				await Task.CompletedTask;
			}
		}

		/// <summary>
		/// 鼠标离开移入框时触发事件
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NullReferenceException"></exception>
		private async Task MouseMoveOut()
		{
			if (_isAutoPopUp)
			{
				if (module != null)
				{
					await module.InvokeVoidAsync("AutoPopoverLocateHidden", null);
					_isPopState = false;
				}
				else
				{
					throw new NullReferenceException();
				}
			}
			else
			{
				await Task.CompletedTask;
			}
		}

		/// <summary>
		/// 默认的鼠标点击触发的事件
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NullReferenceException"></exception>
		private async Task MouseClick()
		{
			if (!_isAutoPopUp)
			{
				if (_isPopState)
				{
					if (module != null)
					{
						await module.InvokeVoidAsync("AutoPopoverLocateHidden", null);
						_isPopState = false;
					}
					else
					{
						throw new NullReferenceException();
					}
				}
				else
				{
					if (module != null)
					{
						await module.InvokeVoidAsync("AutoPopoverLocateShow", PopDirection);
						_isPopState = true;
					}
					else
					{
						throw new NullReferenceException();
					}
				}
			}
			else
			{
				await Task.CompletedTask;
			}
		}

		/// <summary>
		/// 维护私有的鼠标点击事件参数和自动弹出参数
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public override async Task SetParametersAsync(ParameterView parameters)
		{
			bool mouseClickEventExist = parameters.TryGetValue<EventCallback<MouseEventArgs>>("MouseClickEvent", out var mouseClickCallback);
			if (mouseClickEventExist)
			{
				_isAutoPopUp = true;
				_mouseClickEvent = mouseClickCallback;
			}
			if (!mouseClickEventExist)
			{
				if (parameters.TryGetValue<bool>("IsAutoPopUp", out var autoPopUpValue))
				{
					if (!autoPopUpValue)
					{
						_isAutoPopUp = autoPopUpValue;
						_mouseClickEvent = new EventCallback<MouseEventArgs>(this, MouseClick);
					}
				}
			}
			await base.SetParametersAsync(parameters);
		}

		/// <summary>
		/// 导入js
		/// </summary>
		/// <param name="firstRender"></param>
		/// <returns></returns>
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				module = await JS.InvokeAsync<IJSObjectReference>("import", "./_content/SkyBlazor/js/Component/MoveInPopover.js");
			}
			await base.OnAfterRenderAsync(firstRender);
		}
	}
}