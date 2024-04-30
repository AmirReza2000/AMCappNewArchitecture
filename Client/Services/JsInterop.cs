using Microsoft.JSInterop;

namespace Client.Services
{
    public static class JsInterop
    {
        [JSInvokable]
        public static void DifferentMethodName()
        {
            Console.WriteLine("rrrrrrrrrrrrrrrrr");
        }
    }
}
