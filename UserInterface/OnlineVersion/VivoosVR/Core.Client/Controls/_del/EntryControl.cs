using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Core.Client.Controls
{
    public partial class EntryControl : StackPanel
    {
        #region Title
        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(EntryControl), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region LabelMargin
        public static Thickness GetLabelMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(LabelMarginProperty);
        }

        public static void SetLabelMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(LabelMarginProperty, value);
        }

        // Using a DependencyProperty as the backing store for LabelMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelMarginProperty =
            DependencyProperty.RegisterAttached("LabelMargin", typeof(Thickness), typeof(EntryControl), new FrameworkPropertyMetadata(new Thickness(2)));

        public Thickness LabelMargin
        {
            get { return (Thickness)GetValue(LabelMarginProperty); }
            set { SetValue(LabelMarginProperty, value); }
        }
        #endregion

        #region LabelWidth
        public static GridLength GetLabelWidth(DependencyObject obj)
        {
            return (GridLength)obj.GetValue(LabelWidthProperty);
        }

        public static void SetLabelWidth(DependencyObject obj, GridLength value)
        {
            obj.SetValue(LabelWidthProperty, value);
        }

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.RegisterAttached("LabelWidth", typeof(GridLength), typeof(EntryControl), new FrameworkPropertyMetadata(GridLength.Auto));

        public GridLength LabelWidth
        {
            get { return (GridLength)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }
        #endregion

        #region IsRequired
        public static bool GetIsRequired(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsRequiredProperty);
        }

        public static void SetIsRequired(DependencyObject obj, bool value)
        {
            obj.SetValue(IsRequiredProperty, value);
        }

        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.RegisterAttached(
            "IsRequired",
            typeof(bool),
            typeof(EntryControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        } 
        #endregion


        public EntryControl()
        {
            Initialized += Entry_Initialized;
        }

        void Entry_Initialized(object sender, EventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                int counter = 0;

                object oGrid = this.FindName("MainGrid");
                if (oGrid != null) this.Children.Remove((Grid)oGrid);

                Grid grid = new Grid();
                grid.Name = "MainGrid";

                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = LabelWidth });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                while (true)
                {
                    if (Children.Count > 0 && Children[0] is FrameworkElement)
                    {
                        FrameworkElement control = this.Children[0] as FrameworkElement;
                        this.Children.Remove(control);

                        grid.RowDefinitions.Add(new RowDefinition());

                        string title = EntryControl.GetTitle(control);
                        bool isRequired = EntryControl.GetIsRequired(control);

                        Label label = new Label() { Content = title == null ? "" : title };
                        label.Padding = LabelMargin;
                        grid.Children.Add(label);
                        label.SetValue(Grid.ColumnProperty, 0);
                        label.SetValue(Grid.RowProperty, counter);
                        label.Margin = new Thickness(2);
                        label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        label.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        label.FontWeight = isRequired ? FontWeights.Bold : FontWeights.Normal;
                        label.Target = control;

                        grid.Children.Add(control);
                        control.SetValue(Grid.ColumnProperty, 2);
                        control.SetValue(Grid.RowProperty, counter);
                        control.Margin = new Thickness(2);
                        control.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                        counter++;
                    }
                    else break;
                }

                this.Children.Add(grid);
            }
        }
    }
}
