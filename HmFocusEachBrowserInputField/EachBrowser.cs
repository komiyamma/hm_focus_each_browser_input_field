using System;
using System.Windows.Automation;

using System.Text;

namespace HmFocusEachBrowserInputField;

public partial class HmFocusEachBrowserInputField
{
    public static IntPtr FindEdgeWindowHandle(IntPtr hWndHidemaruHandle)
    {
        // ウィンドウハンドルからAutomationElementを取得
        AutomationElement rootElement = AutomationElement.FromHandle(hWndHidemaruHandle);

        if (rootElement == null)
        {
            return IntPtr.Zero;
        }

        // クラス名が "HM32EachBrowserCtrl" の要素を見つける
        AutomationElement hm32EachBrowserCtrl = FindChildByClassName(rootElement, "HM32EachBrowserCtrl");

        if (hm32EachBrowserCtrl == null)
        {
            return IntPtr.Zero;
        }

        // "HM32EachBrowserCtrl" の子孫からクラス名が "Chrome_WidgetWin_0" の要素を探す
        AutomationElement chromeWidgetWin = FindChildByClassName(hm32EachBrowserCtrl, "Chrome_WidgetWin_0");

        if (chromeWidgetWin == null)
        {
            return IntPtr.Zero;
        }

        // "Chrome_WidgetWin_0" のウィンドウハンドルを取得
        IntPtr edgeWindowHandle = new IntPtr((int)chromeWidgetWin.GetCurrentPropertyValue(AutomationElement.NativeWindowHandleProperty));
        return edgeWindowHandle;
    }

    static AutomationElement FindChildByClassName(AutomationElement parent, string className)
    {
        // クラス名を条件とした検索条件を作成
        Condition condition = new PropertyCondition(AutomationElement.ClassNameProperty, className);

        // 子孫を探索
        return parent.FindFirst(TreeScope.Descendants, condition);
    }
}