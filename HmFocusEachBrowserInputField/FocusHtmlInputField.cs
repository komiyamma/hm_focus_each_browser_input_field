using System;
using System.Windows.Automation;

namespace HmFocusEachBrowserInputField;

public partial class HmFocusEachBrowserInputField
{

    public static bool FocusInputField(IntPtr eachBrowserWindowHandle)
    {
        var appWindow = AutomationElement.FromHandle(eachBrowserWindowHandle);


        // WebView2内のDocument（HTML）を探す
        var docElement = appWindow.FindFirst(TreeScope.Descendants,
            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Document));

        if (docElement == null)
        {
            // HTML document in WebView2 not found
            Console.Error.WriteLine("0");
            return false;
        }

        // inputフィールドを探す（例：名前で）
        var inputField = docElement.FindFirst(TreeScope.Descendants,
            new AndCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit),
                new PropertyCondition(AutomationElement.IsKeyboardFocusableProperty, true)
            ));

        if (inputField != null)
        {
            // Console.WriteLine("inputField find");
            // ((InvokePattern)inputField.GetCurrentPattern(InvokePattern.Pattern))?.Invoke();
            try
            {
                inputField.SetFocus();

                // inputフィールドに文字列を入れる
                var valuePattern = (ValuePattern)inputField.GetCurrentPattern(ValuePattern.Pattern);
                if (valuePattern != null)
                {
                    // valuePattern.SetValue("test");

                    return true;
                }

            }
            catch (ElementNotEnabledException errInput)
            {
                // Console.WriteLine("ElementNotEnabledException");
            }

        }
        else
        {
            // Console.WriteLine("no inputField");
        }

        return false;
    }
}
