﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace ReactiveUI.XamlForms.Sample.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{

        AutoSuspendHelper suspendHelper;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			suspendHelper = ReactiveUI.XamlForms.Sample.App.Initialize(this);
			suspendHelper.FinishedLaunching(app, options);


            LoadApplication(new ReactiveUI.XamlForms.Sample.App ());


			base.FinishedLaunching (app, options);
			return true;
		}

		public override void DidEnterBackground(UIApplication application)
		{
			suspendHelper.DidEnterBackground(application);
		}

		public override void OnActivated(UIApplication application)
		{
			suspendHelper.OnActivated(application);
		}
	}
}
