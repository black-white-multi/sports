

namespace ET
{
	public class AppStartInitFinish_ShowUILogin: AEvent<EventType.AppStartInitFinish>
	{
		protected override void Run(EventType.AppStartInitFinish args)
		{
			UIManage.Instance.ShowUI(WindowId.UILogin);
		}
	}
}
