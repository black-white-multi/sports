using System.Linq;
using FairyGUI;

namespace ET
{
	[ObjectSystem]
	public class UIWindowAwakeSystem : AwakeSystem<UIWindow,GObject>
	{
		public override void Awake(UIWindow self,GObject gobject)
		{
			self.Awake(gobject);
		}
	}

	public sealed class UIWindow: Entity,IAwake<GObject>
	{
        public GObject mGObject;
        public void Awake(GObject gobject)
		{
            this.mGObject = gobject;
		}

	    public void Show()
	    {
            Entity[] components = this.Components.Values.ToArray();
	        foreach (Entity ui in components)
	        {
	            UIBaseView view = ui as UIBaseView;
	            view?.Show();
	        }
        }

	    public void Close()
	    {
            Entity[] components = this.Components.Values.ToArray();
            foreach (Entity ui in components)
            {
	            UIBaseView view = ui as UIBaseView;
	            view?.Hiding();
	        }

	        Dispose();
	    }

	    public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
            
            base.Dispose();
		}
        
	}
}