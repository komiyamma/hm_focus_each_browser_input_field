using System;

namespace HmFocusEachBrowserInputField;
public partial class HmFocusEachBrowserInputField
{
    public static int Main(string[] args)
    {
        // 第1引数がウィンドウハンドルの値  
        if (args.Length == 0)
        {
            Console.Error.WriteLine("0");
            return 0;
        }

        IntPtr hWndHidemaruHandle = IntPtr.Zero;
        // 引数をIntPtrに変換  
        // 文字列をIntPtrに  
        if (long.TryParse(args[0], out long handleValue))
        {
            hWndHidemaruHandle = new IntPtr(handleValue);

            var WebView2Handle = FindEdgeWindowHandle(hWndHidemaruHandle);
            // ウィンドウハンドルを取得  
            if (WebView2Handle != IntPtr.Zero)
            {
                bool focus = FocusInputField(WebView2Handle);
                if (focus)
                {
                    Console.WriteLine("1");
                    return 1;
                }
            }
        }
        Console.Error.WriteLine("0");

        return 0;
    }
}
