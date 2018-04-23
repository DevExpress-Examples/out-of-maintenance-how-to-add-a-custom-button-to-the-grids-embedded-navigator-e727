using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.NavigatorButtons;
using DevExpress.XtraGrid;

namespace CustomNavigator {
	public class MyGridControl : GridControl {
		protected override ControlNavigator CreateEmbeddedNavigator() {
			GridControlNavigator nav = new MyEmbeddedNavigator(this);
			nav.SizeChanged += new EventHandler(OnEmbeddedNavigator_SizeChanged);
			nav.Visible = false;
			nav.TabStop = false;
			return nav;
		}
	}

	public class MyEmbeddedNavigator : GridControlNavigator {
		public MyEmbeddedNavigator(GridControl control) : base(control) {}
		protected override NavigatorButtonsBase CreateButtons() {
			return new MyNavigatorButtons(this);
		}
	}

	[TypeConverter("System.ComponentModel.ExpandableObjectConverter, System")]
	public class MyNavigatorButtons : ControlNavigatorButtons {
		public MyNavigatorButtons(INavigatorOwner owner) : base(owner) {}
		protected override NavigatorButtonCollectionBase CreateNavigatorButtonCollection() {
			return new MyNavigatorButtonCollection(this);
		}
	}

	public class MyNavigatorButtonCollection : ControlNavigatorButtonCollection {
		public MyNavigatorButtonCollection(NavigatorButtonsBase buttons) :base(buttons) {}
		protected override void CreateButtons(NavigatorButtonsBase buttons) {
			base.CreateButtons(buttons);
			AddButton(new MyNavigatorCustomButtonHelper(buttons));
		}
	}

	public class MyNavigatorCustomButtonHelper : ControlNavigatorButtonHelperBase {
		public MyNavigatorCustomButtonHelper(NavigatorButtonsBase buttons) : base(buttons) {}
		public override NavigatorButtonType ButtonType {
			get {
				return NavigatorButtonType.Custom;
			}
		}
		public override bool Enabled {
			get {
				return true;
			}
		}
		protected override void DoDataClick() {
			MessageBox.Show("Custom button action");
		}
	}
}