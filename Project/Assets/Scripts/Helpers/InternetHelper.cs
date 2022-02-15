using UnityEngine;

class InternetHelper
{
    public static bool IsInternetOpen => Application.internetReachability != NetworkReachability.NotReachable;
}
