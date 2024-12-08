// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ViewModels.MenuItemViewModel
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.Enums;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Network.ViewModels
{
  public class MenuItemViewModel : ViewModelBase
  {
    public readonly MenuSectionName _attachedSection;
    private SolidColorBrush _titleForeground;
    private SolidColorBrush _iconForeground;
    private int _count;

    public SolidColorBrush TitleForeground
    {
      get => this._titleForeground;
      set
      {
        this._titleForeground = value;
        this.NotifyPropertyChanged(nameof (TitleForeground));
      }
    }

    public SolidColorBrush IconForeground
    {
      get => this._iconForeground;
      set
      {
        this._iconForeground = value;
        this.NotifyPropertyChanged(nameof (IconForeground));
      }
    }

    public int Count
    {
      get => this._count;
      set
      {
        if (this._count == value)
          return;
        this._count = value;
        this.NotifyPropertyChanged("CountVisibility");
        this.NotifyPropertyChanged("CountString");
      }
    }

    public MenuItemViewModel(MenuSectionName attachedSection)
    {
      this._attachedSection = attachedSection;
    }

    public Visibility CountVisibility => this.Count <= 0 ? (Visibility) 1 : (Visibility) 0;

    public string CountString => this.Count.ToString();

    public void UpdateSelectionState(MenuSectionName selectedSection)
    {
      if (selectedSection == this._attachedSection)
      {
        this.TitleForeground = (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"];
        this.IconForeground = this.TitleForeground;
      }
      else
      {
        this.TitleForeground = (SolidColorBrush) ((IDictionary<object, object>) Application.Current.Resources)[(object) "TextColorContent"];
        this.IconForeground = new SolidColorBrush(Colors.Transparent);
      }
    }
  }
}
