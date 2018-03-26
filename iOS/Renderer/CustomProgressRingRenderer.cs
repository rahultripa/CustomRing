using System;
using customProgressRing;
using customProgressRing.iOS.Renderer;
using System;
using System.Drawing;
using CoreGraphics;

using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomProgressRing), typeof(CustomProgressRingRenderer))]
namespace customProgressRing.iOS.Renderer
{
    public class CustomProgressRingRenderer : ViewRenderer<CustomProgressRing, CustomProgressRingRenderer>
    {
        RadialProgressView bigRadialProgressView;
        UILabel lbl;

        private bool _layouted;
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (!_layouted)
            {
                bigRadialProgressView.Center = new CGPoint(Frame.Width / 2, Frame.Height / 2 - 100);
                _layouted = true;
            }
        }

       
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Renderer")
            {
                
                UIColor BgColor = Element.BackColor.ToUIColor();
                CType.width = Element.ProgressRingWidth;
             
                bigRadialProgressView = new RadialProgressView
                {
                    Center = new PointF((float)(Frame.Width / 2), (float)(Frame.Height / 2 - 100)),
                    Transform = CGAffineTransform.MakeScale(.7f, .7f),
                   

                };
                bigRadialProgressView.BGProgressColor = Element.BackColor.ToUIColor();//; Color.FromHex("#0099ff").ToUIColor();  //UIColor.FromRGB(0, 153, 255);
                bigRadialProgressView.LabelTextDelegate = (val) => Math.Floor(100 - val * 100).ToString().PadLeft(2, '0');


               AddSubview(bigRadialProgressView);

                 lbl = new UILabel();
                lbl.Font = UIFont.SystemFontOfSize(Element.LabelFontSize);
                lbl.Center = new PointF(105, 105);
                lbl.Bounds = new RectangleF(0, 0, 100, 100);
                lbl.TextAlignment = UITextAlignment.Center;
                lbl.Hidden = !Element.IsProgressLabel ;
            
                bigRadialProgressView.AddSubview(lbl);
                bigRadialProgressView.BringSubviewToFront(lbl);
                
                bigRadialProgressView.ProgressColor = Element.ProgressColor.ToUIColor();
                float ProgressRadius = (float)Element.ProgressRadius;
                lbl.TextColor = Element.LabelColor.ToUIColor();
               
                 bigRadialProgressView.Transform = CGAffineTransform.MakeScale(ProgressRadius, ProgressRadius); //new CGAffineTransform(ProgressRedius,ProgressRedius);



            }
            if (e.PropertyName == "ProgressValue")
            {
                float ProgressValues = (float)Element.ProgressValue;
                if (ProgressValues <= 1)
                {
                    bigRadialProgressView.Value = ProgressValues;
                    lbl.Text = (ProgressValues * 100).ToString() + "%";
                }




            }
        }

      
    
    }
}
