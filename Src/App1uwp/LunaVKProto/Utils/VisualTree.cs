// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.VisualTree
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Utils
{
  public static class VisualTree
  {
    public static ScrollViewer GetScrollViewer(this DependencyObject element)
    {
      if (element is ScrollViewer)
        return (ScrollViewer) element;
      int childrenCount = VisualTreeHelper.GetChildrenCount(element);
      for (int index = 0; index < childrenCount; ++index)
      {
        ScrollViewer scrollViewer = VisualTreeHelper.GetChild(element, index).GetScrollViewer();
        if (scrollViewer != null)
          return scrollViewer;
      }
      return (ScrollViewer) null;
    }

    public static DependencyObject FindChild<T>(this DependencyObject element)
    {
      if (element is T)
        return element;
      for (int index = 0; index < VisualTreeHelper.GetChildrenCount(element); ++index)
      {
        DependencyObject child = VisualTreeHelper.GetChild(element, index).FindChild<T>();
        if (child != null)
          return child;
      }
      return (DependencyObject) null;
    }

    public static IEnumerable<T> GetLogicalChildrenByType<T>(
      this FrameworkElement parent,
      bool applyTemplates)
      where T : FrameworkElement
    {
      if (applyTemplates && parent is Control)
        ((Control) parent).ApplyTemplate();
      Queue<FrameworkElement> queue = new Queue<FrameworkElement>(((DependencyObject) parent).GetVisualChildren().OfType<FrameworkElement>());
      while (queue.Count > 0)
      {
        FrameworkElement element = queue.Dequeue();
        if (applyTemplates && element is Control)
          ((Control) element).ApplyTemplate();
        if (element is T obj)
          yield return obj;
        foreach (FrameworkElement frameworkElement in ((DependencyObject) element).GetVisualChildren().OfType<FrameworkElement>())
          queue.Enqueue(frameworkElement);
        element = (FrameworkElement) null;
      }
    }

    public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject parent)
    {
      int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
      for (int i = 0; i < childrenCount; i++)
        yield return VisualTreeHelper.GetChild(parent, i);
    }

    public static T GetFirstAncestorOfType<T>(this DependencyObject start) where T : DependencyObject
    {
      return start.GetAncestorsOfType<T>().FirstOrDefault<T>();
    }

    public static IEnumerable<T> GetAncestorsOfType<T>(this DependencyObject start) where T : DependencyObject
    {
      return start.GetAncestors().OfType<T>();
    }

    public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject start)
    {
      for (DependencyObject parent = VisualTreeHelper.GetParent(start); parent != null; parent = VisualTreeHelper.GetParent(parent))
        yield return parent;
    }
  }
}
