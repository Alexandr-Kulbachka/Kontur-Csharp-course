using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public abstract class Command<T>
    {
        protected List<T> _items { get; set; }
        protected int _position { get; set; }
        protected T _item { get; set; }
        public abstract void Execute();
        public abstract void UnDo();
    }

    public class AddCommand<T> : Command<T>
    {
        public AddCommand(List<T> items, T item)
        {
            _items = items;
            _item = item;
            _position = items.Count;
        }

        public override void Execute()
        {
            _items.Add(_item);
        }

        public override void UnDo()
        {
            _items.RemoveAt(_position);
        }
    }

    public class RemoveCommand<T> : Command<T>
    {
        public RemoveCommand(List<T> items, int position)
        {
            _items = items;
            _item = items[position];
            _position = position;
        }

        public override void Execute()
        {
            _items.RemoveAt(_position);
        }

        public override void UnDo()
        {
            _items.Insert(_position, _item);
        }
    }

    public class History<T>
    {
        private LimitedSizeStack<Command<T>> _commandHistory;
        public History(int limit)
        {
            _commandHistory = new LimitedSizeStack<Command<T>>(limit);
        }

        public void AddCommand(List<T> items, T item)
        {
            var newAddCommand = new AddCommand<T>(items, item);
            newAddCommand.Execute();
            _commandHistory.Push(newAddCommand);
        }

        public void RemoveCommand(List<T> items, int position)
        {
            var newRemoveCommand = new RemoveCommand<T>(items, position);
            newRemoveCommand.Execute();
            _commandHistory.Push(newRemoveCommand);
        }

        public void UndoCommand()
        {
            _commandHistory.Pop().UnDo();
        }

        public bool CanUndo()
        {
            return _commandHistory.Count > 0;
        }
    }

    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public History<TItem> History;
        public ListModel(int limit)
        {
            Items = new List<TItem>();
            History = new History<TItem>(limit);
        }

        public void AddItem(TItem item)
        {
            History.AddCommand(Items, item);
        }

        public void RemoveItem(int index)
        {
            History.RemoveCommand(Items, index);
        }

        public bool CanUndo()
        {
            return History.CanUndo();
        }

        public void Undo()
        {
            History.UndoCommand();
        }
    }
}
