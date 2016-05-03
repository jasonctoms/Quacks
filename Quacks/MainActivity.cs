using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections;
using System;

namespace Quacks
{
	[Activity (Label = "Quacks", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			TextView pushOrder = FindViewById<TextView> (Resource.Id.PushOrderText);
			TextView popOrder = FindViewById<TextView> (Resource.Id.PopOrderText);
			Button pushButton = FindViewById<Button>(Resource.Id.PushButton);
			Button stackPopButton = FindViewById<Button>(Resource.Id.SPopButton);
			Button queuePopButton = FindViewById<Button>(Resource.Id.QPopButton);
			Button resetButton = FindViewById<Button>(Resource.Id.ResetButton);

			pushOrder.SetText ( Resource.String.pushText);
			popOrder.SetText (Resource.String.popText);

			string pushString = string.Empty;
			string popString = string.Empty;

			// Create the two stacks
			Stack stackOne = new Stack();
			Stack stackTwo = new Stack();

			pushButton.Click += (object sender, EventArgs e) =>
			{
				if (pushString != string.Empty || popString != string.Empty)
				{
					Toast.MakeText (this, "Push the Refresh button first if you want new values!", ToastLength.Short).Show ();
					return;
				}
				Random rnd = new Random ();

				for (int i = 0; i < 5; i++)
				{
					int num = rnd.Next (10);
					stackOne.Push (num);
					pushString += Convert.ToString (num) + " ";
				}
				pushOrder.Text = pushString + " ---- numbers pushed.";
			};

			stackPopButton.Click += (object sender, EventArgs e) =>
			{
				if (stackOne.Count == 0) 
				{
					Toast.MakeText (this, "You can't pop before you push!", ToastLength.Short).Show ();
					return;
				}
				for (int i = 0; i < 5; i++)
				{
					int num = (int) stackOne.Pop ();
					popString += Convert.ToString (num) + " ";
				}
				popOrder.Text = popString + " ---- numbers popped";
			};

			queuePopButton.Click += (object sender, EventArgs e) =>
			{
				if (stackTwo.Count == 0) 
				{
					if (stackOne.Count == 0) 
					{
						Toast.MakeText (this, "You can't pop before you push!", ToastLength.Short).Show ();
						return;
					}
					while (stackOne.Count != 0) 
					{
						stackTwo.Push(stackOne.Pop());
					}
					for (int i = 0; i < 5; i++)
					{
						int num = (int) stackTwo.Pop ();
						popString += Convert.ToString (num) + " ";
					}
					popOrder.Text = popString + " ---- numbers popped";
				}
			};

			resetButton.Click += (object sender, EventArgs e) =>
			{
				stackOne.Clear();
				stackTwo.Clear();
				pushOrder.SetText ( Resource.String.pushText);
				popOrder.SetText (Resource.String.popText);
				pushString = string.Empty;
				popString = string.Empty;

			};
		
		}


	}
}


