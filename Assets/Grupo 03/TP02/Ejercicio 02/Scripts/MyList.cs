using System;

namespace MyLinkedList
{
    public class MyNode<T>
    {
        public T Data;
        public MyNode<T> Next;
        public MyNode<T> Previous;

        public MyNode(T data)
        {
            Data = data;
        }

        public override string ToString() => Data?.ToString() ?? "null";
        public bool IsEquals(T value) => Data.Equals(value);
    }

    public class MyList<T>
    {
        private MyNode<T> root;
        private MyNode<T> tail;
        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                MyNode<T> current = root;
                for (int i = 0; i < index; i++) current = current.Next;
                return current.Data;
            }
        }

        public void Add(T value)
        {
            MyNode<T> newNode = new MyNode<T>(value);
            if (root == null)
                root = tail = newNode;
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            Count++;
        }

        public void AddRange(MyList<T> values)
        {
            for (int i = 0; i < values.Count; i++)
                Add(values[i]);
        }

        public void AddRange(T[] values)
        {
            foreach (var v in values) Add(v);
        }

        public bool Remove(T value)
        {
            MyNode<T> current = root;
            while (current != null)
            {
                if (current.IsEquals(value))
                {
                    if (current.Previous != null)
                        current.Previous.Next = current.Next;
                    else
                        root = current.Next;

                    if (current.Next != null)
                        current.Next.Previous = current.Previous;
                    else
                        tail = current.Previous;

                    Count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count) return;
            MyNode<T> current = root;
            for (int i = 0; i < index; i++) current = current.Next;

            if (current.Previous != null)
                current.Previous.Next = current.Next;
            else
                root = current.Next;

            if (current.Next != null)
                current.Next.Previous = current.Previous;
            else
                tail = current.Previous;

            Count--;
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count) return;
            MyNode<T> newNode = new MyNode<T>(value);

            if (index == Count)
            {
                Add(value);
                return;
            }

            MyNode<T> current = root;
            for (int i = 0; i < index; i++) current = current.Next;

            newNode.Next = current;
            newNode.Previous = current.Previous;

            if (current.Previous != null)
                current.Previous.Next = newNode;
            else
                root = newNode;

            current.Previous = newNode;
            Count++;
        }

        public bool IsEmpty() => Count == 0;

        public void Clear()
        {
            root = null;
            tail = null;
            Count = 0;
        }

        public override string ToString()
        {
            string result = "[";
            MyNode<T> current = root;
            while (current != null)
            {
                result += current.Data + (current.Next != null ? ", " : "");
                current = current.Next;
            }
            result += "]";
            return result;
        }

        private void SwapData(MyNode<T> a, MyNode<T> b)
        {
            T temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }

        public void BubbleSort()
        {
            if (Count < 2 || !(root.Data is IComparable<T>)) return;
            for (int i = 0; i < Count - 1; i++)
            {
                MyNode<T> current = root;
                while (current != null && current.Next != null)
                {
                    if (((IComparable<T>)current.Data).CompareTo(current.Next.Data) > 0)
                        SwapData(current, current.Next);
                    current = current.Next;
                }
            }
        }

        public void SelectionSort()
        {
            if (Count < 2 || !(root.Data is IComparable<T>)) return;
            MyNode<T> current = root;
            while (current != null)
            {
                MyNode<T> min = current;
                MyNode<T> runner = current.Next;
                while (runner != null)
                {
                    if (((IComparable<T>)runner.Data).CompareTo(min.Data) < 0)
                        min = runner;
                    runner = runner.Next;
                }
                if (min != current)
                    SwapData(current, min);
                current = current.Next;
            }
        }

        public void InsertionSort()
        {
            if (Count < 2 || !(root.Data is IComparable<T>)) return;
            MyNode<T> current = root.Next;
            while (current != null)
            {
                T key = current.Data;
                MyNode<T> mover = current.Previous;
                while (mover != null && ((IComparable<T>)mover.Data).CompareTo(key) > 0)
                {
                    mover.Next.Data = mover.Data;
                    mover = mover.Previous;
                }
                if (mover != null)
                    mover.Next.Data = key;
                else
                    root.Data = key;
                current = current.Next;
            }
        }
    }
}
