﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using _1RM.Model;
using _1RM.View;
using _1RM.View.ServerList;

namespace _1RM.Controls
{
    /// <summary>
    /// Interaction logic for ServerListItem.xaml
    /// </summary>
    public partial class ServerListItem : UserControl
    {
        public static readonly DependencyProperty ProtocolServerViewModelProperty =
            DependencyProperty.Register("ProtocolBaseViewModel", typeof(ProtocolBaseViewModel), typeof(ServerListItem),
                new PropertyMetadata(null, new PropertyChangedCallback(OnDataChanged)));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (ProtocolBaseViewModel)e.NewValue;
            ((ServerListItem)d).DataContext = value;
        }

        public ProtocolBaseViewModel? ProtocolBaseViewModel
        {
            get => GetValue(ProtocolServerViewModelProperty) as ProtocolBaseViewModel;
            set => SetValue(ProtocolServerViewModelProperty, value);
        }


        public ServerListItem()
        {
            InitializeComponent();
            PopupCardSettingMenu.Closed += (sender, args) =>
            {
                ProtocolBaseViewModel?.ClearActions();
            };
        }

        private void BtnSettingMenu_OnClick(object sender, RoutedEventArgs e)
        {
            ProtocolBaseViewModel?.BuildActions();
            PopupCardSettingMenu.IsOpen = true;
        }

        private void ServerMenuButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button { CommandParameter: ProtocolAction afs })
            {
                afs.Run();
            }
            PopupCardSettingMenu.IsOpen = false;
        }

        private void ItemsCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            ServerListPageView.ItemsCheckBox_OnClick_Static(sender, e);
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // stop right click edit 
            if (e.ChangedButton == MouseButton.Right)
            {
                e.Handled = true;
            }
        }
    }
}
