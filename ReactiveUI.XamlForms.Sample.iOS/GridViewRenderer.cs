using Cirrious.FluentLayouts.Touch;
using Foundation;
using ReactiveUI.XamlForms.Sample;
using ReactiveUI.XamlForms.Sample.iOS;
using ReactiveUI.XamlForms.Sample.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GridView), typeof(GridViewRenderer))]
namespace ReactiveUI.XamlForms.Sample.iOS
{
    public class GridViewRenderer
     : ViewRenderer<GridView, GridViewCollectionView>, IEnableLogger
    {
        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e)
        {            
            //base.OnElementChanged(e);
            if (Control == null)
            {
                SecondPageViewModel vm = e.NewElement.BindingContext as SecondPageViewModel;
                var layout = new UICollectionViewFlowLayout() { ScrollDirection = UICollectionViewScrollDirection.Vertical };
                var view = new GridViewCollectionView(
                    new CoreGraphics.CGRect(this.Element.X, this.Element.Y, (float)this.Element.Width, (float)this.Element.Height), layout);

                view.RegisterClassForCell(typeof(GridViewCollectionViewCell), GridViewCollectionViewCell.gridViewCellId);
                view.ViewModel = (SecondPageViewModel)e.NewElement?.BindingContext;
                SetNativeControl(view);
            }

        }
    }

   

}