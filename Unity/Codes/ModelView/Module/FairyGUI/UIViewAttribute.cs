using System;

namespace ET
{
	[AttributeUsage(AttributeTargets.Class)]
	public class UIViewAttribute: Attribute
	{
		public string Type { get; private set; }

		public UIViewAttribute(string type)
		{
			this.Type = type;
		}
	}
}