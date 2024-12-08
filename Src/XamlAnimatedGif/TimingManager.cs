
// Type: XamlAnimatedGif.TimingManager
// Assembly: XamlAnimatedGif, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02CD40CB-7FCE-4EFE-9D7D-B0095CDCA3E7
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\XamlAnimatedGif.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace XamlAnimatedGif
{
  internal class TimingManager
  {
    private readonly List<TimeSpan> _timeSpans = new List<TimeSpan>();
    private int _current;
    private int _count;
    private bool _isComplete;
    private TimeSpan _elapsed;
    private readonly Task _completedTask = (Task) Task.FromResult<int>(0);
    private TaskCompletionSource<int> _pauseCompletionSource;

    public TimingManager(RepeatBehavior repeatBehavior) => this.RepeatBehavior = repeatBehavior;

    public RepeatBehavior RepeatBehavior { get; set; }

    public void Add(TimeSpan timeSpan) => this._timeSpans.Add(timeSpan);

    public async Task<bool> NextAsync(CancellationToken cancellationToken)
    {
      if (this.IsComplete)
        return false;
      await this.IsPausedAsync(cancellationToken);
      RepeatBehavior repeatBehavior = this.RepeatBehavior;
      TimeSpan ts = this._timeSpans[this._current];
      await Task.Delay(ts, cancellationToken);
      ++this._current;
      this._elapsed += ts;
      if (repeatBehavior.HasDuration && this._elapsed >= repeatBehavior.Duration)
      {
        this.IsComplete = true;
        return false;
      }
      if (this._current < this._timeSpans.Count)
        return true;
      ++this._count;
      if (repeatBehavior.HasCount)
      {
        if ((double) this._count < repeatBehavior.Count)
        {
          this._current = 0;
          return true;
        }
        this.IsComplete = true;
        return false;
      }
      this._current = 0;
      return true;
    }

    public void Reset()
    {
      this._current = 0;
      this._count = 0;
      this._elapsed = TimeSpan.Zero;
      this.IsComplete = false;
    }

    public event EventHandler Completed;

    protected virtual void OnCompleted() => this.Completed((object) this, EventArgs.Empty);

    public bool IsComplete
    {
      get => this._isComplete;
      private set
      {
        this._isComplete = value;
        if (!value)
          return;
        this.OnCompleted();
      }
    }

    public void Pause()
    {
      this.IsPaused = true;
      this._pauseCompletionSource = new TaskCompletionSource<int>();
    }

    public void Resume()
    {
      this._pauseCompletionSource.TrySetResult(0);
      this._pauseCompletionSource = (TaskCompletionSource<int>) null;
      this.IsPaused = false;
    }

    public bool IsPaused { get; private set; }

    private Task IsPausedAsync(CancellationToken cancellationToken)
    {
      TaskCompletionSource<int> tcs = this._pauseCompletionSource;
      if (tcs == null)
        return this._completedTask;
      if (cancellationToken.CanBeCanceled)
        cancellationToken.Register((Action) (() => tcs.TrySetCanceled()));
      return (Task) tcs.Task;
    }
  }
}
