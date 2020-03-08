using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private LinkedList<T> stack;
        private readonly int limit;
        public LimitedSizeStack(int limit)
        {
            this.limit = limit;
            stack = new LinkedList<T>();
        }

        public void Push(T item)
        {
            if (stack.Count >= limit)
                stack.Remove(stack.First);
            stack.AddLast(item);
        }

        public T Pop()
        {
            var lastElement = stack.Last;
            stack.RemoveLast();
            return lastElement.Value;
        }

        public int Count
        {
            get
            {
                return stack.Count;
            }
        }
    }
}
