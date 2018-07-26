using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculateContent.iOS;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TableView), typeof(TableViewRendererForiOS))]
namespace CalculateContent.iOS
{
    public class TableViewRendererForiOS : TableViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
        {
            base.OnElementChanged(e);

            Control.ScrollEnabled = false;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            Element.HeightRequest = Control.ContentSize.Height;
        }
    }
}