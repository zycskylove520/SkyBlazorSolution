using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;

namespace SkyBlazor.Component
{
	public partial class MoveInPopover
	{
		[Inject]
		private IJSRuntime JS { get; set; } = default!;
		private IJSObjectReference? module;

		public enum Direction
		{
			Left,
			Right,
			Top,
			Bottom
		}
		
		[Parameter]
		public Direction PopDirection { get; set; } = Direction.Bottom;
		//
		// 摘要:
		//		设置移入框部件
		[Parameter]
		[NotNull]
		public RenderFragment? HeadContent { get; set; }

		//
		// 摘要:
		//     获得弹出框部件
		[Parameter]
		[NotNull]
		public RenderFragment? PopoverContent { get; set; }

		//
		// 摘要:
		//     添加额外属性
		[Parameter(CaptureUnmatchedValues = true)]
		public IDictionary<string, object>? AdditionalAttributes { get; set; }

		//
		// 摘要:
		//     点击事件
		[Parameter]
		public EventCallback<MouseEventArgs> MouseClickEvent { get; set; }

		public async Task MouseMoveIn()
		{
			await module!.InvokeVoidAsync("AutoPopoverLocateShow", PopDirection);
		}

		public async Task MouseMoveOut()
		{
			await module!.InvokeVoidAsync("AutoPopoverLocateHidden", null);
		}

		protected override Task OnInitializedAsync()
		{
			return base.OnInitializedAsync();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				module = await JS.InvokeAsync<IJSObjectReference>("import", "../Common/MoveInPopover.razor.js");
				await base.OnAfterRenderAsync(firstRender);
			}
		}
	}
}
