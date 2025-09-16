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

        // BubbleSort
        public void BubbleSort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (((IComparable<T>)this[j]).CompareTo(this[j + 1]) > 0)
                    {
                        // Intercambiar valores
                        T temp = this[j];
                        SetAt(j, this[j + 1]);
                        SetAt(j + 1, temp);
                    }
                }
            }
        }

        // SelectionSort
        public void SelectionSort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (((IComparable<T>)this[j]).CompareTo(this[minIdx]) < 0)
                    {
                        minIdx = j;
                    }
                }
                if (minIdx != i)
                {
                    T temp = this[i];
                    SetAt(i, this[minIdx]);
                    SetAt(minIdx, temp);
                }
            }
        }

        // InsertionSort
        public void InsertionSort()
        {
            for (int i = 1; i < Count; i++)
            {
                T key = this[i];
                int j = i - 1;
                while (j >= 0 && ((IComparable<T>)this[j]).CompareTo(key) > 0)
                {
                    SetAt(j + 1, this[j]);
                    j--;
                }
                SetAt(j + 1, key);
            }
        }

        // Método auxiliar para modificar el valor en un índice
        private void SetAt(int index, T value)
        {
            MyNode<T> current = root;
            for (int i = 0; i < index; i++)
                current = current.Next;
            current.Data = value;
        }
    }
}

