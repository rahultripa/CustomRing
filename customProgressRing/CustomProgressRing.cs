using System;
using Xamarin.Forms;

namespace customProgressRing
{
    public class CustomProgressRing : View
    {
        public static readonly BindableProperty ProgressValueProperty =
            BindableProperty.Create<CustomProgressRing, double>
                  (p => p.ProgressValue, double.NaN, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
                  {
                  var control = (CustomProgressRing)bindable;
                      control.ProgressValue = newValue;

                  });

        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }


        public static readonly BindableProperty ProgressRadiusProperty =
            BindableProperty.Create<CustomProgressRing, double>
           (p => p.ProgressRadius, 0.7, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
           {
                    var control = (CustomProgressRing)bindable;
               control.ProgressRadius = newValue;

           });

        public double ProgressRadius
        {
            get { return (double)GetValue(ProgressRadiusProperty); }
            set { SetValue(ProgressRadiusProperty, value); }
        }



        public static readonly BindableProperty ProgressColorProperty =
            BindableProperty.Create("ProgressColor", typeof(Color), typeof(CustomProgressRing), Color.White, BindingMode.TwoWay, null, null);
        public Color ProgressColor
        {
            get { return (Color)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }


        public static readonly BindableProperty LabelFontSizeProperty =
            BindableProperty.Create("LabelFontSize", typeof(int), typeof(CustomProgressRing), 8, BindingMode.TwoWay, null, null);
        public float LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        public static readonly BindableProperty IsProgressLabelProperty =
            BindableProperty.Create("IsProgressLabel", typeof(bool), typeof(CustomProgressRing), true, BindingMode.TwoWay, null, null);
        public bool IsProgressLabel
        {
            get { return (bool)GetValue(IsProgressLabelProperty); }
            set { SetValue(IsProgressLabelProperty, value); }
        }
       
        public static readonly BindableProperty ProgressRingWidthProperty =
            BindableProperty.Create("ProgressRingWidth", typeof(int), typeof(CustomProgressRing), 6, BindingMode.TwoWay, null, null);
        public int ProgressRingWidth
        {
            get { return (int)GetValue(ProgressRingWidthProperty); }
            set { SetValue(ProgressRingWidthProperty, value); }
        }
       

        public static readonly BindableProperty BackColorProperty =
            BindableProperty.Create("BackColor", typeof(Color), typeof(CustomProgressRing), Color.Blue, BindingMode.TwoWay, null, null);
        public Color BackColor
        {
            get { return (Color)GetValue(BackColorProperty); }
            set { SetValue(BackColorProperty, value); }
        }

        public static readonly BindableProperty LabelColorProperty =
            BindableProperty.Create("BackColor", typeof(Color), typeof(CustomProgressRing), Color.Black, BindingMode.TwoWay, null, null);
        public Color LabelColor
        {
            get { return (Color)GetValue(LabelColorProperty); }
            set { SetValue(LabelColorProperty, value); }
        }



        public event EventHandler ValueChanged;
        public void NotifyValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
        }
    }
}
