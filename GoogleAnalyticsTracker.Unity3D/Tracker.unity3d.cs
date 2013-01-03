using System;

namespace GoogleAnalyticsTracker
{
    public partial class Tracker
    {
#if UNITY3D
        public Tracker(string trackingAccount, string trackingDomain)
			// partials.. should work in unity currently not...
			//: this(trackingAccount, trackingDomain, new Unity3DAnalyticsSession())
        {
			
        }
#endif
    }
}
