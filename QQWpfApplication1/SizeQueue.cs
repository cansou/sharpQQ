using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QQWpfApplication1
{
    class SizeQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();
        private readonly int maxSize;
        public SizeQueue(int maxSize) { this.maxSize = maxSize; }

        public void Enqueue(T item)
        {
            lock (queue)
            {
                while (queue.Count >= maxSize)
                {
                    Monitor.Wait(queue);
                }
                Console.WriteLine("en 1");
                queue.Enqueue(item);
                Console.WriteLine("en 2");
                if (queue.Count == 1)
                {
                    // wake up any blocked dequeue
                    Monitor.PulseAll(queue);
                }
            }
        }
        public T Dequeue()
        {
            lock (queue)
            {
                Console.WriteLine("de 1");
                while (queue.Count == 0)
                {
                    Monitor.Wait(queue);
                }
                Console.WriteLine("de 2");
                T item = queue.Dequeue();
                if (queue.Count == maxSize - 1)
                {
                    // wake up any blocked enqueue
                    Monitor.PulseAll(queue);
                }
                return item;
            }
        }

        bool closing;
        public void Close()
        {
            lock (queue)
            {
                closing = true;
                Monitor.PulseAll(queue);
            }
        }
        public bool TryDequeue(out T value)
        {
            lock (queue)
            {
                while (queue.Count == 0)
                {
                    if (closing)
                    {
                        value = default(T);
                        return false;
                    }
                    Monitor.Wait(queue);
                }
                value = queue.Dequeue();
                if (queue.Count == maxSize - 1)
                {
                    // wake up any blocked enqueue
                    Monitor.PulseAll(queue);
                }
                return true;
            }
        }
    }
}
