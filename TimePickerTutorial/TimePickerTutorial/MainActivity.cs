using Android.App;
using Android.Widget;
using Android.OS;

using System;
using Android.Util;
using Android.Text.Format;

namespace TimePickerTutorial
{
    [Activity(Label = "TimePickerTutorial", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView timeDisplay;
        Button timeSelectButton;

        TextView date_display;
        Button select_date_button;
               
        public TextView combinedTextView;    //public static TextView combinedTextView;
        public static DateTime combinedDateTime;
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            timeDisplay = FindViewById<TextView>(Resource.Id.time_display);
            timeSelectButton = FindViewById<Button>(Resource.Id.select_button);

            date_display = FindViewById<TextView>(Resource.Id.date_display);
            select_date_button = FindViewById<Button>(Resource.Id.select_date_button);

            combinedTextView = FindViewById<TextView>(Resource.Id.combined_date_time);
            combinedDateTime = DateTime.Now;
            string combinedDateTimeString = combinedDateTime.ToString();

            combinedTextView.Text = combinedDateTimeString;            

            timeSelectButton.Click += TimeSelectOnClick;
            select_date_button.Click += Select_date_button_Click;
        }//
       

        public void TimeSelectOnClick(object sender, EventArgs eventArgs)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(
                delegate (DateTime time) 
                {
                    timeDisplay.Text = time.ToShortTimeString();
                });

            frag.Show(FragmentManager, TimePickerFragment.TAG);
            //combinedTextView.Text = combinedDateTime.ToLongTimeString();  // "time selected working";              ... not needed ??
        }


        public void Select_date_button_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                date_display.Text = time.ToLongDateString();
                //_dateDisplay.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
            //combinedTextView.Text = "date selected working";   ... not needed ??
        }


        public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
        {
            // TAG can be any string of your choice.     
            public static readonly string TAG = "X:" + typeof (DatePickerFragment).Name.ToUpper();
            // Initialize this value to prevent NullReferenceExceptions.     
            Action<DateTime> _dateSelectedHandler = delegate { };

            public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
            {
                DatePickerFragment frag = new DatePickerFragment();
                frag._dateSelectedHandler = onDateSelected;
                return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                DateTime currently = DateTime.Now;
                DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                                this,
                                                                currently.Year,
                                                                currently.Month - 1,
                                                                currently.Day);
                return dialog;
            }

            public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
            {
                // Note: monthOfYear is a value between 0 and 11, not 1 and 12!         
                DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
                Log.Debug(TAG, selectedDate.ToLongDateString());
                _dateSelectedHandler(selectedDate);

                //  my stuff
                combinedDateTime = new DateTime(year, monthOfYear+1, dayOfMonth, combinedDateTime.Hour, combinedDateTime.Minute, combinedDateTime.Second);               
                TextView combinedTextView = Activity.FindViewById<TextView>(Resource.Id.combined_date_time);
                combinedTextView.Text = combinedDateTime.ToString();      
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------

       






        public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener
        {
            public static readonly string TAG = "MyTimePickerFragment";
            Action<DateTime> timeSelectedHandler = delegate { };

            public static TimePickerFragment NewInstance(Action<DateTime> onTimeSelected)
            {
                TimePickerFragment frag = new TimePickerFragment();               
                frag.timeSelectedHandler = onTimeSelected; return frag;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                DateTime currentTime = DateTime.Now;
                bool is24HourFormat = DateFormat.Is24HourFormat(Activity);
                //is24HourFormat = true;
                TimePickerDialog dialog = new TimePickerDialog
                    (Activity, this, currentTime.Hour, currentTime.Minute, is24HourFormat);
                return dialog;
            }

            public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
            {
                DateTime currentTime = DateTime.Now;
                DateTime selectedTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hourOfDay, minute, 0);
                Log.Debug(TAG, selectedTime.ToLongTimeString());
                timeSelectedHandler(selectedTime);

                //  my stuff
                combinedDateTime = new DateTime(combinedDateTime.Year, combinedDateTime.Month, combinedDateTime.Day, hourOfDay, minute, 0);               
                TextView combinedTextView = Activity.FindViewById<TextView>(Resource.Id.combined_date_time);
                combinedTextView.Text = combinedDateTime.ToString();
            }
        }
    }
}

