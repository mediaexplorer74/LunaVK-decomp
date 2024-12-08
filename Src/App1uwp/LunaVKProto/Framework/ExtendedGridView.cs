// Decompiled with JetBrains decompiler
// Type: App1uwp.Framework.ExtendedGridView
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Library;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace App1uwp.Framework
{
  public class ExtendedGridView : GridView
  {
    private ScrollViewer scrollViewer;
    private bool InLoading;

    protected virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this.scrollViewer = ((Control) this).GetTemplateChild("ScrollViewer") as ScrollViewer;
      ScrollViewer scrollViewer = this.scrollViewer;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<ScrollViewerViewChangedEventArgs>>(new Func<EventHandler<ScrollViewerViewChangedEventArgs>, EventRegistrationToken>(scrollViewer.add_ViewChanged), new Action<EventRegistrationToken>(scrollViewer.remove_ViewChanged), new EventHandler<ScrollViewerViewChangedEventArgs>(this.scrollViewer_ViewChanged));
    }

    private async void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
      if (!e.IsIntermediate || this.InLoading)
        return;
      ScrollViewer sv = sender as ScrollViewer;
      bool res = false;
      res = sv.VerticalScrollMode != null ? sv.ScrollableHeight - sv.VerticalOffset < 700.0 : sv.ScrollableWidth - sv.HorizontalOffset < 700.0;
      if (!res || !(((FrameworkElement) this).DataContext is ISupportLoadMore) || !(((FrameworkElement) this).DataContext as ISupportLoadMore).HasMoreItems)
        return;
      this.InLoading = true;
      await (((FrameworkElement) this).DataContext as ISupportLoadMore).LoadData();
      this.InLoading = false;
    }
  }
}
