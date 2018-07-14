using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

// from https://www.youtube.com/watch?v=3eUz0q-NKio
// by EDMT DEV  (not from MSDN network)

namespace MultiChoiceDialog_not_MSDN
{
    [Activity(Label = "MultiChoiceDialog_not_MSDN", MainLauncher = true)]
    public class MainActivity : Activity, IDialogInterfaceOnClickListener, IDialogInterfaceOnMultiChoiceClickListener
    {
        static string[] choices = {
            "Android", "iOS", "UWP", "Xamarin"
        };
        bool[] itemsChecked = new bool[choices.Length];
        
        public void OnClick(IDialogInterface dialog, int which)
        {
            if (which < 0)  //  "OK" button
                Toast.MakeText(this, "OK clicked", ToastLength.Short).Show();
            else if (which > 0)  // "Cancel" button
                Toast.MakeText(this, "Cancel clicked", ToastLength.Short).Show();

        }

        public void OnClick(IDialogInterface dialog, int which, bool isChecked)
        {
            Toast.MakeText(this, choices[which] + (isChecked? " checked" : " unchecked"), ToastLength.Short).Show();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += delegate
            {
                ShowDialog(0);

            };
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case 0:
                    return new AlertDialog.Builder(this)
                        .SetIcon(Resource.Drawable.musicalnotesheadphoneColor)  // set icon for dialog
                        .SetTitle("Multi Choices Dialog") // set title for dialog
                        .SetPositiveButton("OK", this)  // need implement IDialogInterface.OnClick
                        .SetNegativeButton("Cancel", this)  // same as before
                        .SetMultiChoiceItems(choices, itemsChecked, this)  // Need implement IDialogInterface.OnMultiChoicesClickListener  
                        .Create();
                    break;

                default:
                    break;
            }
            return null;
        }



    }
}

