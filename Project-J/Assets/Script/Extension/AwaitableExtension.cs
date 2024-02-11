using UnityEngine;

public static class AwaitableExtension
{
    public static void Forget(this Awaitable _) { }
}

public static class AwaitableHelper
{
    public static Awaitable Completed()
    {
        var complete = new AwaitableCompletionSource();
        complete.SetResult();
        return complete.Awaitable;
    }
}