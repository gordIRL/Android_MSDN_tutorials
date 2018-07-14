using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using System.Collections.Generic;
using Android.Views;
using Android.Content;

namespace MultiChoice_BETTER_not_MSDN
{
    [Activity(Label = "MultiChoice_BETTER_not_MSDN", Theme = "@style/AppTheme",  MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IDialogInterfaceOnClickListener, IDialogInterfaceOnMultiChoiceClickListener
    {
        Button mOrder;
        TextView mItemSelected;
        String[] listItems;
        bool[] checkedItems;
        List<int> mUserItems = new List<int>();

        public void OnClick(IDialogInterface dialog, int position, bool isChecked)
        {
            if (isChecked)
            {
                mUserItems.Add(position);
            }
            else
            {
                mUserItems.Remove(position);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mOrder = FindViewById<Button>(Resource.Id.btnOrder);
            mItemSelected = FindViewById<TextView>(Resource.Id.tvItemSelected);

            listItems = Resources.GetStringArray(Resource.Array.shopping_item);
            checkedItems = new bool[listItems.Length];

            mOrder.Click += delegate
            {
                Android.Support.V7.App.AlertDialog.Builder mBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                mBuilder.SetTitle(Resource.String.dialog_title);

                mBuilder.SetMultiChoiceItems(listItems, checkedItems, IDialogInterfaceOnClickListener)
                {

                });


            };

        }
    }
}

