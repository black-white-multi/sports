using System.Threading.Tasks;
using FairyGUI;

namespace ET
{
    public class UIBaseView: Entity, IAwake
    {
        public GObject GObject;
        public GComponent mPanel;

        public UIWindowType mWindowType = UIWindowType.Normal;
        public UIWindowShowMode mShowMode = UIWindowShowMode.DoNothing;

        public virtual void Awake()
        {
            this.GObject = (this.Parent as UIWindow).mGObject;
            mPanel = this.GObject?.asCom;

            InitUI();
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
        public virtual void Close()
        {
            object[] attrs = this.GetType().GetCustomAttributes(typeof(UIViewAttribute), false);
            if (attrs.Length == 1)
            {
                UIViewAttribute attribute = attrs[0] as UIViewAttribute;
                UIMangage.Instance.CloseUI(attribute.Type);
            }
        }

        /// <summary>
        /// UI初始化方法。_必须
        /// </summary>
        public virtual void InitUI()
        {

        }

        public virtual void DoShowAnimationEvent()
        {

        }

        public virtual Task HideAnimationEvent()
        {
            return Task.CompletedTask;
        }

        private async void DoHideAnimationEvent()
        {
            await HideAnimationEvent();

            this.GObject?.Dispose();
            this.Dispose();
        }

        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Show()
        {
            this.DoShowAnimationEvent();
        }

        /// <summary>
        /// 隐藏状态(窗口关闭了)
        /// </summary>
        public virtual void Hiding()
        {
            this.DoHideAnimationEvent();
        }

        /// <summary>
        /// 再显示状态
        /// </summary>
        public virtual void ReDisplay()
        {
            this.GObject.visible = true;
        }

        /// <summary>
        /// 冻结状态（在栈集合中）
        /// </summary>
        public virtual void Freeze()
        {
            this.GObject.visible = false;
        }
        
    }
}