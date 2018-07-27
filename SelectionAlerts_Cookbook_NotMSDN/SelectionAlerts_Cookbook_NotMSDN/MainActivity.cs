using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace SelectionAlerts_Cookbook_NotMSDN
{
    [Activity(Label = "SelectionAlerts_Cookbook_NotMSDN", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button button;
        Button btnDisplay;
        TextView textView1;
        List<string> selectedCurrencyList = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            textView1 = FindViewById<TextView>(Resource.Id.textView1);

            btnDisplay = FindViewById<Button>(Resource.Id.btnDisplay);
            btnDisplay.Click += delegate
            {
                // clear textview
                textView1.Text = "";

                // iterate through selectedCurrencyList and display in the textview
                foreach (var item in selectedCurrencyList)
                {
                    textView1.Text += item + "\n";
                }
            };

            button = FindViewById<Button>(Resource.Id.button1);
            button.Click += delegate
            {

                // clear user's selection to begin
                selectedCurrencyList.Clear();

                // list of items for user to select from
                string[] items = { "EUR", "GBP", "CAD", "USD", "YUAN", "FRC", "LYP", "VIX" };

                // bool array for selected checkboxes in MultiItemSelect 
                bool[] selected = new bool[items.Length];



                using (var dialog = new AlertDialog.Builder(this)){
                    dialog.SetTitle("Alert Title");
                    dialog.SetPositiveButton("Close", delegate{
                         });

                    // Sets whether this dialog is cancelable with the Android.Views.KeyEvent.KEYCODE_BACK key.
                    //dialog.SetCancelable(true);


                    // check all boxes and add all items to list(s)
                    dialog.SetNeutralButton("ALL", delegate
                    {
                        // clear list 1st to avoid getting duplicate entries
                        selectedCurrencyList.Clear();

                        // set all items in bool[] selected to TRUE
                        for (int i = 0; i < selected.Length; i++)
                        {
                            selected[i] = true;
                            selectedCurrencyList.Add(items[i]);
                        }
                    });

                    // deselect all boxes & clear list
                    dialog.SetNegativeButton("Clear", delegate
                    {
                        selectedCurrencyList.Clear();
                    });

                    


                    //// Set.Items() method   (Displays checkboxes)
                    //dialog.SetItems(items, (s, e) =>
                    //{
                    //    int index = e.Which;
                    //});

                    //// Adapter method (Doesn't display checkboxes!!)
                    //var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);
                    //dialog.SetAdapter(adapter, (s, e) =>
                    //{
                    //    var index = e.Which;
                    //});


                    //// Single select Radio Button
                    //int selected = -1;
                    //dialog.SetSingleChoiceItems(items, selected, (s, e) =>
                    //{
                    //    selected = e.Which;
                    //    Toast.MakeText(this, "You selected: " + items[selected], ToastLength.Short).Show();
                    //});


                    //// Multi-Select Checklist - Original
                    //bool[] selected = new bool[items.Length];
                    //dialog.SetMultiChoiceItems(items, selected, 
                    //    (s, e) =>  {
                    //        int index = e.Which;
                    //        bool isChecked = e.IsChecked;
                    //        selected[index] = isChecked;
                    //        Toast.MakeText(this, "You selected: " + items[index], ToastLength.Short).Show();
                    //        selectedCurrencyList.Add(items[index]);
                    //});


                    // Multi-Select Checklist - EXperiments
                    //bool[] selected = new bool[items.Length];                   


                    //// set all items in bool[] to FALSE
                    //for (int i = 0; i < selected.Length; i++)
                    //{
                    //    selected[i] = false;
                    //    selectedCurrencyList.Remove(items[i]);
                    //}


                    dialog.SetMultiChoiceItems(items, selected,
                        (s, e) => {
                            int index = e.Which;
                            bool isChecked = e.IsChecked;
                            selected[index] = isChecked;
                            Toast.MakeText(this, "You clicked: " + items[index]
                                + "\nChecked: " + e.IsChecked, ToastLength.Short).Show();

                            // add item to list if now selected - ie isChecked is now TRUE  
                            if (isChecked)
                                selectedCurrencyList.Add(items[index]);                            
                            else                            
                                selectedCurrencyList.Remove(items[index]);                          
                        });        

                    dialog.Show();

                }// end Using
            };// end delegate


        }
    }
}

