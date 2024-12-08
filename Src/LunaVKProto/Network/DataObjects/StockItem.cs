// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.DataObjects.StockItem
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using App1uwp.Network.json;
using App1uwp.Network.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable disable
namespace App1uwp.Network.DataObjects
{
  public class StockItem : ViewModelBase
  {
    private bool _isSelected;

    public StoreProduct product { get; set; }

    public string description { get; set; }

    public string author { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool can_purchase { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool free { get; set; }

    public string payment_type { get; set; }

    public int price { get; set; }

    public string price_str { get; set; }

    public string photo_35 { get; set; }

    public string photo_70 { get; set; }

    public string photo_140 { get; set; }

    public string photo_296 { get; set; }

    public string photo_592 { get; set; }

    public string background { get; set; }

    public List<string> demo_photos_560 { get; set; }

    [JsonConverter(typeof (VKBooleanConverter))]
    public bool @new { get; set; }

    public SolidColorBrush TabBackground
    {
      get
      {
        return !this.IsSelected ? new SolidColorBrush(Colors.Transparent) : ((IDictionary<object, object>) Application.Current.Resources)[(object) "AccentBrushHigh"] as SolidColorBrush;
      }
    }

    public bool IsSelected
    {
      get => this._isSelected;
      set
      {
        this._isSelected = value;
        this.NotifyPropertyChanged<SolidColorBrush>((Expression<Func<SolidColorBrush>>) (() => this.TabBackground));
      }
    }
  }
}
