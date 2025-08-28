using System;
using System.Windows.Automation;

namespace HmFocusEachBrowserInputField;

public partial class HmFocusEachBrowserInputField
{

    public static bool FocusInputField(IntPtr eachBrowserWindowHandle, IntPtr dummy, String tempFilePath)
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

                if (String.IsNullOrEmpty(tempFilePath))
                {
                    Console.Error.WriteLine("1");
                    return true;
                }
                // inputフィールドに文字列を入れる
                var valuePattern = (ValuePattern)inputField.GetCurrentPattern(ValuePattern.Pattern);
                if (valuePattern != null)
                {
                    try
                    {
                        // ファイルの読み込み
                        string text = System.IO.File.ReadAllText(tempFilePath);
                        valuePattern.SetValue(text);

                        // ファイルの書き込み
                        System.IO.File.WriteAllText(tempFilePath, "HmConvAiWeb.Complete(2);\n");

                        inputField.SetFocus();
                        Console.Error.WriteLine("2");
                        return true;
                    }
                    catch (Exception) { }

                    try
                    {
                        System.IO.File.WriteAllText(tempFilePath, "HmConvAiWeb.Complete(1);\n");
                        Console.Error.WriteLine("1");
                        return true;
                    }
                    catch (Exception) {
                    }

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
