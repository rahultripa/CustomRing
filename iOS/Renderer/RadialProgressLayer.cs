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
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
#endif
namespace customProgressRing.iOS.Renderer
{
    internal abstract class RadialProgressLayer : CALayer
    {
        protected const float FullCircleAngle = (float)Math.PI * 2;

        protected CGPoint CenterPoint;
        UIImage fullProgressImage;

        protected CGSize BoundsSize { get; set; }
        public CGRect BackBounds { get; set; }
        public nfloat Percentage { get; set; }
       
        protected nfloat endRadius;
        protected nfloat startRadius;
        protected nfloat backgroundWidth;
        protected nfloat progressLayerWidth;

        static readonly UIColor DefaultFillColor = Color.FromHex("#404143").ToUIColor();   //UIColor.FromRGB(0, 153, 255);
        static readonly UIColor DefaultBGFillColor = Color.FromHex("#404143").ToUIColor();   //UIColor.FromRGB(0, 153, 255);

      //  protected static readonly CGColor BackBorderColor = Color.FromHex("#000000").ToUIColor().CGColor;            //UIColor.Black.CGColor;
        protected static readonly CGColor BackBorderColor = Color.FromHex("#404143").ToUIColor().CGColor;            //UIColor.Black.CGColor;

        protected static readonly CGColor BackInnerBorderColor = Color.FromHex("#404143").ToUIColor().CGColor;
        protected static readonly CGColor BackCircleBackgroundColor = Color.FromHex("#404143").ToUIColor().CGColor;  //UIColor.Black.CGColor;

        UIColor colors;

        public UIColor Colors
        {
            get { return colors ?? DefaultFillColor; }
            set
            {
                if (colors != value)
                {
                    colors = value;
                    fullProgressImage = GenerateFullProgressImage();
                }
            }
        }

        UIColor bgcolors;
        private nfloat layerwidth;
        public nfloat Layerwidth
        {
            get { return layerwidth; }
            set
            {
                if (layerwidth != value)
                {
                    layerwidth = value;

               }
            }
        }

        public UIColor BGColors
        {
            get { return bgcolors ?? DefaultBGFillColor; }
            set
            {
                if (bgcolors != value)
                {
                    bgcolors = value;

                }
            }
        }
        public RadialProgressLayer(nfloat startRadius, nfloat endRadius, nfloat backgroundWidth, nfloat progressLayerWidth)
            : this()
        {
            this.startRadius = startRadius  ;
            this.endRadius = endRadius ;
            this.backgroundWidth = backgroundWidth ;
            this.progressLayerWidth = progressLayerWidth   ;
            nfloat width = Layerwidth;
            Bounds = new CGRect(CGPoint.Empty, new CGSize(progressLayerWidth , progressLayerWidth ));
            BackBounds = new CGRect(CGPoint.Empty, new CGSize(backgroundWidth , backgroundWidth));

            CenterPoint = new CGPoint(Bounds.GetMidX(), Bounds.GetMidY());
            fullProgressImage = GenerateFullProgressImage();
        }

        public RadialProgressLayer()
        {
         //   BackgroundColor = Color.FromHex("#ffffff").ToUIColor().ColorWithAlpha(0.0f).CGColor;  //UIColor.Clear.CGColor;
            ContentsScale = UIScreen.MainScreen.Scale;
        }


        public override void DrawInContext(CGContext context)
        {
            var progressAngle = CalculateProgressAngle(Percentage);

            using (var path = BezierPathGenerator.Bagel(CenterPoint, startRadius, endRadius, 0f, progressAngle))
            {
                context.AddPath(path.CGPath);
                context.Clip();
                context.DrawImage(Bounds, fullProgressImage.CGImage);
            }
        }

        protected virtual UIImage GenerateFullProgressImage()
        {
            UIImage resultImage;


            resultImage = UIGraphics.GetImageFromCurrentImageContext();

            return resultImage;
        }


        nfloat CalculateProgressAngle(nfloat percentage)
        {
            return (nfloat)Math.PI / 50f * percentage;
        }

        public abstract UIImage GenerateBackgroundImage();
    }
}
