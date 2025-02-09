using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
#nullable disable
namespace Blazor.Extensions
{
    public class BECanvasComponent : ComponentBase
    {
        [Parameter]
        public long Height { get; set; }

        [Parameter]
        public long Width { get; set; }

        protected readonly string Id = Guid.NewGuid().ToString();
        protected ElementReference _canvasRef;

        public ElementReference CanvasReference => this._canvasRef;

        [Inject]
        internal IJSRuntime JSRuntime { get; set; }
    }
}
