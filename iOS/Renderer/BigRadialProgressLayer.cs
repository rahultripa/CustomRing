
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

#if __UNIFIED__
using UIKit;
using Foundation;
using CoreGraphics;
using CoreAnimation;
#else
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
#endif
namespace customProgressRing.iOS.Renderer
{
    internal class BigRadialProgressLayer : RadialProgressLayer
    {
        const float GlowOffset = 1f;
        const float GlowRadius = 9f;
        const float BorderPadding = 5f;
        const float EndBorderRadius = 105f;
        const float StartBorderRadius = 70f;

        static readonly CGColor GlowColor = Color.FromHex("#ffffff").ToUIColor().CGColor;   //UIColor.White.CGColor;
        static readonly CGColor GradientOverlayStartColor = Color.FromHex("#ffffff").ToUIColor().CGColor;    //UIColor.White.CGColor;
        static readonly CGColor GradientOverlayEndColor = Color.FromHex("#ffffff").ToUIColor().ColorWithAlpha(0.0f).CGColor;  //UIColor.Clear.CGColor;

        public BigRadialProgressLayer(int i)
            : base(startRadius: i,
                    endRadius:90,
                    backgroundWidth: 214f,
                    progressLayerWidth: 200f)
        {
        }

        protected override UIImage GenerateFullProgressImage()
        {
            UIImage resultImage;

            UIGraphics.BeginImageContextWithOptions(Bounds.Size, false, UIScreen.MainScreen.Scale);

            using (var context = UIGraphics.GetCurrentContext())
            using (var path = BezierPathGenerator.Bagel(CenterPoint, startRadius , endRadius, 0f, FullCircleAngle))
            {
                    context.SaveState();
                  context.SetFillColor(Colors.CGColor);
                    context.AddPath(path.CGPath);
                    context.FillPath();

                    context.RestoreState();

                    context.SaveState();
                    context.AddPath(path.CGPath);
                    context.Clip();

                    context.RestoreState();

                resultImage = UIGraphics.GetImageFromCurrentImageContext();
            }

            return resultImage;
        }

        public override UIImage GenerateBackgroundImage()
        {
            UIImage resultImage;

            UIGraphics.BeginImageContextWithOptions(BackBounds.Size, false, UIScreen.MainScreen.Scale);
            var center = new CGPoint(BackBounds.GetMidX(), BackBounds.GetMidY());

            using (var context = UIGraphics.GetCurrentContext())
            using (var innerBorderBagelPath = BezierPathGenerator.Bagel(center,startRadius, endRadius, 0f, FullCircleAngle))
            {

                context.SaveState();
                context.SetFillColor(BGColors.CGColor);
                innerBorderBagelPath.Fill();
                context.RestoreState();

                resultImage = UIGraphics.GetImageFromCurrentImageContext();
            }

            return resultImage;
        }


    }
}
