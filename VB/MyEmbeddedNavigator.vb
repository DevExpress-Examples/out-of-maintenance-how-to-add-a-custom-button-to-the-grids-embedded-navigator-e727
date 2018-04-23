Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.NavigatorButtons
Imports DevExpress.XtraGrid

Namespace CustomNavigator
	Public Class MyGridControl
		Inherits GridControl
		Protected Overrides Function CreateEmbeddedNavigator() As ControlNavigator
			Dim nav As GridControlNavigator = New MyEmbeddedNavigator(Me)
			AddHandler nav.SizeChanged, AddressOf OnEmbeddedNavigator_SizeChanged
			nav.Visible = False
			nav.TabStop = False
			Return nav
		End Function
	End Class

	Public Class MyEmbeddedNavigator
		Inherits GridControlNavigator
		Public Sub New(ByVal control As GridControl)
			MyBase.New(control)
		End Sub
		Protected Overrides Function CreateButtons() As NavigatorButtonsBase
			Return New MyNavigatorButtons(Me)
		End Function
	End Class

	<TypeConverter("System.ComponentModel.ExpandableObjectConverter, System")> _
	Public Class MyNavigatorButtons
		Inherits ControlNavigatorButtons
		Public Sub New(ByVal owner As INavigatorOwner)
			MyBase.New(owner)
		End Sub
		Protected Overrides Function CreateNavigatorButtonCollection() As NavigatorButtonCollectionBase
			Return New MyNavigatorButtonCollection(Me)
		End Function
	End Class

	Public Class MyNavigatorButtonCollection
		Inherits ControlNavigatorButtonCollection
		Public Sub New(ByVal buttons As NavigatorButtonsBase)
			MyBase.New(buttons)
		End Sub
		Protected Overrides Sub CreateButtons(ByVal buttons As NavigatorButtonsBase)
			MyBase.CreateButtons(buttons)
			AddButton(New MyNavigatorCustomButtonHelper(buttons))
		End Sub
	End Class

	Public Class MyNavigatorCustomButtonHelper
		Inherits ControlNavigatorButtonHelperBase
		Public Sub New(ByVal buttons As NavigatorButtonsBase)
			MyBase.New(buttons)
		End Sub
		Public Overrides ReadOnly Property ButtonType() As NavigatorButtonType
			Get
				Return NavigatorButtonType.Custom
			End Get
		End Property
		Public Overrides ReadOnly Property Enabled() As Boolean
			Get
				Return True
			End Get
		End Property
		Protected Overrides Sub DoDataClick()
			MessageBox.Show("Custom button action")
		End Sub
	End Class
End Namespace