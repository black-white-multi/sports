using System;

namespace ET
{
	[AttributeUsage(AttributeTargets.Class)]
	public class UIViewAttribute: Attribute
	{
		public WindowId WindowId { 
			get; 
			private set; 
		}

		public UIViewAttribute(WindowId windowId)
		{
			this.WindowId = windowId;
		}
	}
}