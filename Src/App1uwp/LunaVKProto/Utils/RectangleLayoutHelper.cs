// Decompiled with JetBrains decompiler
// Type: App1uwp.Utils.RectangleLayoutHelper
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;

#nullable disable
namespace App1uwp.Utils
{
  public class RectangleLayoutHelper
  {
    public static List<Rect> CreateLayout(
      double parent_width,
      double parent_height,
      List<Size> childrenRects,
      double marginBetween)
    {
      List<ThumbAttachment> thumbAttachments = RectangleLayoutHelper.ConvertSizesToThumbAttachments(childrenRects);
      RectangleLayoutHelper.ProcessThumbnails(parent_width, parent_height, thumbAttachments, marginBetween);
      return RectangleLayoutHelper.ConvertProcessedThumbsToRects(thumbAttachments, marginBetween, parent_width);
    }

    private static List<Rect> ConvertProcessedThumbsToRects(
      List<ThumbAttachment> thumbs,
      double marginBetween,
      double width)
    {
      List<Rect> rects = new List<Rect>(thumbs.Count);
      double y = 0.0;
      double widthOfRow = RectangleLayoutHelper.CalculateWidthOfRow(thumbs, marginBetween);
      double num = width / 2.0 - widthOfRow / 2.0;
      double x = num;
      for (int index = 0; index < thumbs.Count; ++index)
      {
        ThumbAttachment thumb = thumbs[index];
        rects.Add(new Rect(x, y, thumb.CalcWidth, thumb.CalcHeight));
        if (!thumb.LastColumn && !thumb.LastRow)
          x += thumb.CalcWidth + marginBetween;
        else if (thumb.LastRow)
          y += thumb.CalcHeight + marginBetween;
        else if (thumb.LastColumn)
        {
          x = num;
          y += thumb.CalcHeight + marginBetween;
        }
      }
      return rects;
    }

    private static double CalculateWidthOfRow(List<ThumbAttachment> thumbs, double marginBetween)
    {
      double widthOfRow = 0.0;
      foreach (ThumbAttachment thumb in thumbs)
      {
        widthOfRow += thumb.CalcWidth;
        widthOfRow += marginBetween;
        if (!thumb.LastRow)
        {
          if (thumb.LastColumn)
            break;
        }
        else
          break;
      }
      if (widthOfRow > 0.0)
        widthOfRow -= marginBetween;
      return widthOfRow;
    }

    private static double calculateMultiThumbsHeight(
      List<double> ratios,
      double width,
      double margin)
    {
      return (width - (double) (ratios.Count - 1) * margin) / ratios.Sum();
    }

    private static void ProcessThumbnails(
      double max_width,
      double max_height,
      List<ThumbAttachment> thumbs,
      double marginBetween)
    {
      string str = "";
      int[] numArray = new int[3];
      List<double> source = new List<double>();
      int count = thumbs.Count;
      bool flag = false;
      foreach (ThumbAttachment thumb in thumbs)
      {
        double ratio = thumb.getRatio();
        if (ratio == -1.0)
          flag = true;
        char orient = ratio > 1.2 ? 'w' : (ratio < 0.8 ? 'n' : 'q');
        str += orient.ToString();
        ++numArray[RectangleLayoutHelper.oi(orient)];
        source.Add(ratio);
      }
      if (flag)
        return;
      double num1 = source.Count > 0 ? source.Sum() / (double) source.Count : 1.0;
      double margin = marginBetween;
      double width1;
      double num2;
      if (max_width > 0.0)
      {
        width1 = max_width;
        num2 = max_height;
      }
      else
      {
        width1 = 320.0;
        num2 = 210.0;
      }
      double num3 = width1 / num2;
      switch (count)
      {
        case 1:
          if (source[0] > 0.8)
          {
            thumbs[0].SetViewSize(width1, width1 / source[0], false, false);
            break;
          }
          thumbs[0].SetViewSize(num2 * source[0], num2, false, false);
          break;
        case 2:
          if (str == "ww" && num1 > 1.4 * num3 && source[1] - source[0] < 0.2)
          {
            double height = Math.Min(width1 / source[0], Math.Min(width1 / source[1], (num2 - marginBetween) / 2.0));
            thumbs[0].SetViewSize(width1, height, true, false);
            thumbs[1].SetViewSize(width1, height, false, false);
            break;
          }
          if (str == "ww" || str == "qq")
          {
            double width2 = (width1 - margin) / 2.0;
            double height = Math.Min(width2 / source[0], Math.Min(width2 / source[1], num2));
            thumbs[0].SetViewSize(width2, height, false, false);
            thumbs[1].SetViewSize(width2, height, false, false);
            break;
          }
          double width3 = (width1 - margin) / source[1] / (1.0 / source[0] + 1.0 / source[1]);
          double width4 = width1 - width3 - margin;
          double height1 = Math.Min(num2, Math.Min(width3 / source[0], width4 / source[1]));
          thumbs[0].SetViewSize(width3, height1, false, false);
          thumbs[1].SetViewSize(width4, height1, false, false);
          break;
        case 3:
          if (str == "www")
          {
            double height2 = Math.Min(width1 / source[0], (num2 - marginBetween) * 0.66);
            thumbs[0].SetViewSize(width1, height2, true, false);
            double width5 = (width1 - margin) / 2.0;
            double height3 = Math.Min(num2 - height2 - marginBetween, Math.Min(width5 / source[1], width5 / source[2]));
            thumbs[1].SetViewSize(width5, height3, false, false);
            thumbs[2].SetViewSize(width5, height3, false, false);
            break;
          }
          double height4 = num2;
          double width6 = Math.Min(height4 * source[0], (width1 - margin) * 0.75);
          thumbs[0].SetViewSize(width6, height4, false, false);
          double height5 = source[1] * (num2 - marginBetween) / (source[2] + source[1]);
          double height6 = num2 - height5 - marginBetween;
          double width7 = Math.Min(width1 - width6 - margin, Math.Min(height5 * source[2], height6 * source[1]));
          thumbs[1].SetViewSize(width7, height6, false, true);
          thumbs[2].SetViewSize(width7, height5, false, true);
          break;
        case 4:
          if (str == "wwww")
          {
            double width8 = width1;
            double height7 = Math.Min(width8 / source[0], (num2 - marginBetween) * 0.66);
            thumbs[0].SetViewSize(width8, height7, true, false);
            double val2 = (width1 - 2.0 * margin) / (source[1] + source[2] + source[3]);
            double width9 = val2 * source[1];
            double width10 = val2 * source[2];
            double width11 = val2 * source[3];
            double height8 = Math.Min(num2 - height7 - marginBetween, val2);
            thumbs[1].SetViewSize(width9, height8, false, false);
            thumbs[2].SetViewSize(width10, height8, false, false);
            thumbs[3].SetViewSize(width11, height8, false, false);
            break;
          }
          double height9 = num2;
          double width12 = Math.Min(height9 * source[0], (width1 - margin) * 0.66);
          thumbs[0].SetViewSize(width12, height9, false, false);
          double val2_1 = (num2 - 2.0 * marginBetween) / (1.0 / source[1] + 1.0 / source[2] + 1.0 / source[3]);
          double height10 = val2_1 / source[1];
          double height11 = val2_1 / source[2];
          double height12 = val2_1 / source[3];
          double width13 = Math.Min(width1 - width12 - margin, val2_1);
          thumbs[1].SetViewSize(width13, height10, false, true);
          thumbs[2].SetViewSize(width13, height11, false, true);
          thumbs[3].SetViewSize(width13, height12, false, true);
          break;
        default:
          List<double> doubleList1 = new List<double>();
          if (num1 > 1.1)
          {
            foreach (double val2_2 in source)
              doubleList1.Add(Math.Max(1.0, val2_2));
          }
          else
          {
            foreach (double val2_3 in source)
              doubleList1.Add(Math.Min(1.0, val2_3));
          }
          Dictionary<string, List<double>> dictionary = new Dictionary<string, List<double>>();
          dictionary[string.Concat((object) count)] = new List<double>()
          {
            RectangleLayoutHelper.calculateMultiThumbsHeight(doubleList1, width1, margin)
          };
          for (int index = 1; index <= count - 1; ++index)
            dictionary[index.ToString() + "," + (object) (count - index)] = new List<double>()
            {
              RectangleLayoutHelper.calculateMultiThumbsHeight(doubleList1.Sublist<double>(0, index), width1, margin),
              RectangleLayoutHelper.calculateMultiThumbsHeight(doubleList1.Sublist<double>(index, doubleList1.Count), width1, margin)
            };
          for (int index1 = 1; index1 <= count - 2; ++index1)
          {
            for (int index2 = 1; index2 <= count - index1 - 1; ++index2)
              dictionary[index1.ToString() + "," + (object) index2 + "," + (object) (count - index1 - index2)] = new List<double>()
              {
                RectangleLayoutHelper.calculateMultiThumbsHeight(doubleList1.Sublist<double>(0, index1), width1, margin),
                RectangleLayoutHelper.calculateMultiThumbsHeight(doubleList1.Sublist<double>(index1, index1 + index2), width1, margin),
                RectangleLayoutHelper.calculateMultiThumbsHeight(doubleList1.Sublist<double>(index1 + index2, doubleList1.Count), width1, margin)
              };
          }
          string key1 = (string) null;
          double num4 = 0.0;
          foreach (string key2 in dictionary.Keys)
          {
            List<double> doubleList2 = dictionary[key2];
            double num5 = marginBetween * (double) (doubleList2.Count - 1);
            foreach (double num6 in doubleList2)
              num5 += num6;
            double num7 = Math.Abs(num5 - num2);
            if (key2.IndexOf(",") != -1)
            {
              string[] strArray = key2.Split(',');
              if (int.Parse(strArray[0]) > int.Parse(strArray[1]) || strArray.Length > 2 && int.Parse(strArray[1]) > int.Parse(strArray[2]))
                num7 *= 1.1;
            }
            if (key1 == null || num7 < num4)
            {
              key1 = key2;
              num4 = num7;
            }
          }
          List<ThumbAttachment> thumbAttachmentList1 = new List<ThumbAttachment>((IEnumerable<ThumbAttachment>) thumbs);
          List<double> doubleList3 = new List<double>((IEnumerable<double>) doubleList1);
          string[] strArray1 = key1.Split(',');
          List<double> doubleList4 = dictionary[key1];
          int index3 = 0;
          for (int index4 = 0; index4 < strArray1.Length; ++index4)
          {
            int num8 = int.Parse(strArray1[index4]);
            List<ThumbAttachment> thumbAttachmentList2 = new List<ThumbAttachment>();
            for (int index5 = 0; index5 < num8; ++index5)
            {
              thumbAttachmentList2.Add(thumbAttachmentList1[0]);
              thumbAttachmentList1.RemoveAt(0);
            }
            double num9 = doubleList4[index3];
            ++index3;
            int num10 = thumbAttachmentList2.Count - 1;
            for (int index6 = 0; index6 < thumbAttachmentList2.Count; ++index6)
            {
              ThumbAttachment thumbAttachment = thumbAttachmentList2[index6];
              double num11 = doubleList3[0];
              doubleList3.RemoveAt(0);
              double width14 = num11 * num9;
              double height13 = num9;
              int num12 = index6 == num10 ? 1 : 0;
              thumbAttachment.SetViewSize(width14, height13, num12 != 0, false);
            }
          }
          break;
      }
    }

    private static int oi(char orient)
    {
      if (orient == 'n')
        return 1;
      return orient == 'q' ? 2 : 0;
    }

    private static List<ThumbAttachment> ConvertSizesToThumbAttachments(List<Size> childrenRects)
    {
      return childrenRects.Select<Size, ThumbAttachment>((Func<Size, ThumbAttachment>) (r => new ThumbAttachment()
      {
        Height = r.Height > 0.0 ? r.Height : 100.0,
        Width = r.Width > 0.0 ? r.Width : 100.0
      })).ToList<ThumbAttachment>();
    }
  }
}
