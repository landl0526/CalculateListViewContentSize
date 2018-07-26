using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CalculateContent.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TableView), typeof(TableViewRendererForAndroid))]
namespace CalculateContent.Droid
{
    public class TableViewRendererForAndroid : TableViewRenderer
    {
        public TableViewRendererForAndroid(Context context) : base(context)
        {
            SetWillNotDraw(false);
        }
        private int _mPosition;
        public override bool DispatchTouchEvent(MotionEvent e)
        {
            if (e.ActionMasked == MotionEventActions.Down)
            {
                _mPosition = this.Control.PointToPosition((int)e.GetX(), (int)e.GetY());
                return base.DispatchTouchEvent(e);
            }

            if (e.ActionMasked == MotionEventActions.Move)
            {
                return true;
            }

            if (e.ActionMasked == MotionEventActions.Up)
            {
                if (this.Control.PointToPosition((int)e.GetX(), (int)e.GetY()) == _mPosition)
                {
                    base.DispatchTouchEvent(e);
                }
                else
                {
                    Pressed = false;
                    Invalidate();
                    return true;
                }
            }

            return base.DispatchTouchEvent(e);
        }

        private LayoutParams parameter;
        private int oldCount = 0;
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            var listAdapter = Control.Adapter;
            int totalHeight = Control.PaddingTop + Control.PaddingBottom;
            int desiredWidth = MeasureSpec.MakeMeasureSpec(Control.Width, MeasureSpecMode.AtMost);
            for (int i = 0; i < Control.Count; i++)
            {
                Android.Views.View listItem = listAdapter.GetView(i, null, Control);

                if (listItem != null)
                {
                    // This next line is needed before you call measure or else you won't get measured height at all. The listitem needs to be drawn first to know the height.
                    listItem.LayoutParameters = new Android.Widget.RelativeLayout.LayoutParams(-2, -2);
                    listItem.Measure(desiredWidth, 0);
                    totalHeight += listItem.MeasuredHeight;
                }
            }

            LayoutParams param = Control.LayoutParameters;
            Element.HeightRequest = totalHeight + (Control.DividerHeight * (Control.Count - 1));
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);


            var listAdapter = Control.Adapter;
            int totalHeight = Control.PaddingTop + Control.PaddingBottom;
            int desiredWidth = MeasureSpec.MakeMeasureSpec(Control.Width, MeasureSpecMode.AtMost);
            for (int i = 0; i < Control.Count; i++)
            {
                Android.Views.View listItem = listAdapter.GetView(i, null, Control);

                if (listItem != null)
                {
                    // This next line is needed before you call measure or else you won't get measured height at all. The listitem needs to be drawn first to know the height.
                    listItem.LayoutParameters = new Android.Widget.RelativeLayout.LayoutParams(-2, -2);
                    listItem.Measure(desiredWidth, 0);
                    totalHeight += listItem.MeasuredHeight;
                }
            }

            LayoutParams param = Control.LayoutParameters;
            Element.HeightRequest = totalHeight + (Control.DividerHeight * (Control.Count - 1));
        }
    }
}