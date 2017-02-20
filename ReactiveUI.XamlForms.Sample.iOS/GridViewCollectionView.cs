using Cirrious.FluentLayouts.Touch;
using Foundation;
using ReactiveUI.XamlForms.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ReactiveUI.XamlForms.Sample.iOS
{
    public class GridViewCollectionView : ReactiveCollectionView<SecondPageViewModel>
    {
        public GridViewCollectionView(CoreGraphics.CGRect frame, UICollectionViewLayout layout) : base(frame, layout)
        {
            this.BackgroundColor = UIColor.White;
            this.OneWayBind(ViewModel, x => x.GridViewItems, x => x.DataSource,
                (items) =>
                {
                    return new ReactiveUI.ReactiveCollectionViewSource<GridViewItem>(
                        this,
                        items,
                        GridViewCollectionViewCell.gridViewCellId,
                        (viewCell) =>
                        {

                        });
                }
            );
        }
    }


    public class GridViewCollectionViewCell : ReactiveCollectionViewCell<GridViewItem>
    {
        static public NSString gridViewCellId = new NSString("GridViewCell");

        UILabel indexLabel;


        public GridViewCollectionViewCell()
        {
            Setup();
        }

        public GridViewCollectionViewCell(IntPtr ptr) : base(ptr)
        {
            Setup();
        }

        void Setup()
        {


            indexLabel = new UILabel();
            this.OneWayBind(ViewModel, vm => vm.Index, view => view.indexLabel.Text,
                            selector: (arg) =>
                            {
                                return arg.ToString();
                            });

            ContentView.AddSubview(indexLabel);

            //Uses https://www.nuget.org/packages/Cirrious.FluentLayout/
            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                indexLabel.WithSameTop(this),
                indexLabel.WithSameLeft(this),
                indexLabel.WithSameRight(this),
                indexLabel.WithSameBottom(this)
            );
            indexLabel.Frame = ContentView.Bounds;
            indexLabel.TextColor = UIColor.Red;
        }


        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
        }
    }

}