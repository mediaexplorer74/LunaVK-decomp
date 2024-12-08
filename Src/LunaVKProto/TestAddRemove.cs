// Decompiled with JetBrains decompiler
// Type: App1uwp.TestAddRemove
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace App1uwp
{
  public sealed class TestAddRemove : PageBase, IComponentConnector
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public ObservableCollection<TestAddRemove.Temp> Items { get; set; }

    public TestAddRemove()
    {
      this.InitializeComponent();
      this.Items = new ObservableCollection<TestAddRemove.Temp>();
      ((FrameworkElement) this).put_DataContext((object) this);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      this.Items.Add(new TestAddRemove.Temp(this.Items.Count, "lol"));
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) => this.Items.RemoveAt(1);

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
      this.Items.Insert(2, new TestAddRemove.Temp(this.Items.Count, "lol"));
    }

    private void Button_Click_3(object sender, RoutedEventArgs e)
    {
      TestAddRemove.Temp temp = this.Items[2];
      this.Items.RemoveAt(2);
      this.Items.Insert(4, temp);
    }

    private void Button_Click_4(object sender, RoutedEventArgs e) => this.Items.Move(1, 5);

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///TestAddRemove.xaml"), (ComponentResourceLocation) 0);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    public void Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ButtonBase buttonBase1 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase1.add_Click), new Action<EventRegistrationToken>(buttonBase1.remove_Click), new RoutedEventHandler(this.Button_Click));
          break;
        case 2:
          ButtonBase buttonBase2 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase2.add_Click), new Action<EventRegistrationToken>(buttonBase2.remove_Click), new RoutedEventHandler(this.Button_Click_1));
          break;
        case 3:
          ButtonBase buttonBase3 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase3.add_Click), new Action<EventRegistrationToken>(buttonBase3.remove_Click), new RoutedEventHandler(this.Button_Click_2));
          break;
        case 4:
          ButtonBase buttonBase4 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase4.add_Click), new Action<EventRegistrationToken>(buttonBase4.remove_Click), new RoutedEventHandler(this.Button_Click_3));
          break;
        case 5:
          ButtonBase buttonBase5 = (ButtonBase) target;
          WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase5.add_Click), new Action<EventRegistrationToken>(buttonBase5.remove_Click), new RoutedEventHandler(this.Button_Click_4));
          break;
      }
      this._contentLoaded = true;
    }

    public class Temp
    {
      public int id { get; set; }

      public string content { get; set; }

      public Temp(int _id, string _content)
      {
        this.id = _id;
        this.content = _content;
      }
    }
  }
}
