
using System;

namespace MyLinkedList
{
    // Nodo de la lista
    public class MyNode<T>
    {
        public T Data;
        public MyNode<T> Next;
        public MyNode<T> Previous;

        public MyNode(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }

        public override string ToString()
        {
            return Data != null ? Data.ToString() : "null";
        }

        public bool IsEquals(T value)
        {
            return Data.Equals(value);
        }
    }

    // Lista doblemente enlazada
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
                for (int i = 0; i < index; i++)
                    current = current.Next;
                return current.Data;
            }
        }

        public void Add(T value)
        {
            MyNode<T> newNode = new MyNode<T>(value);
            if (root == null)
            {
                root = newNode;
                tail = newNode;
            }
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
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

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
            if (index < 0 || index > Count) throw new IndexOutOfRangeException();

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
    }
}

