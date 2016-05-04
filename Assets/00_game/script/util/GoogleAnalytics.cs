using UnityEngine;
using System.Collections;

public class GoogleAnalytics : GoogleAnalyticsBase<GoogleAnalytics> {

	public override void Initialize ()
	{
		base.Initialize ();
		propertyID_Android = "UA-77286676-1";
		propertyID_iOS = "UA-77286676-2";

		bundleID = "jp.everystudio.pocket.zoo";
		appName = "ポケット動物園";
		appVersion = "1";
	}

}
