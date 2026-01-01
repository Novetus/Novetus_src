using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

//https://stackoverflow.com/questions/28028262/using-await-semaphoreslim-waitasync-in-net-4

public class AsyncSemaphore
{
    private readonly static Task s_completed = Task.Factory.StartNew(() => true);
    private readonly Queue<TaskCompletionSource<bool>> m_waiters = new Queue<TaskCompletionSource<bool>>();
    private int m_currentCount;

    public AsyncSemaphore(int initialCount)
    {
        if (initialCount < 0) throw new ArgumentOutOfRangeException("initialCount");
        m_currentCount = initialCount;
    }

    public Task WaitAsync()
    {
        lock (m_waiters)
        {
            if (m_currentCount > 0)
            {
                --m_currentCount;
                return s_completed;
            }
            else
            {
                var waiter = new TaskCompletionSource<bool>();
                m_waiters.Enqueue(waiter);
                return waiter.Task;
            }
        }
    }
    public void Release()
    {
        TaskCompletionSource<bool> toRelease = null;
        lock (m_waiters)
        {
            if (m_waiters.Count > 0)
                toRelease = m_waiters.Dequeue();
            else
                ++m_currentCount;
        }
        if (toRelease != null)
            toRelease.SetResult(true);
    }
}

//https://stackoverflow.com/questions/7612602/why-cant-i-use-the-await-operator-within-the-body-of-a-lock-statement/50139704#50139704

public class SemaphoreLocker
{
    private readonly AsyncSemaphore _semaphore = new AsyncSemaphore(1);

    public async Task LockAsync(Func<Task> worker)
    {
        await _semaphore.WaitAsync();
        try
        {
            await worker();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // overloading variant for non-void methods with return type (generic T)
    public async Task<T> LockAsync<T>(Func<Task<T>> worker)
    {
        await _semaphore.WaitAsync();
        try
        {
            return await worker();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}