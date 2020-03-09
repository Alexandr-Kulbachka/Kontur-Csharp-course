using System;
using System.Collections.Generic;

namespace Clones
{
    public class StackItem<T>
    {
        public T Value { get; set; }
        public StackItem<T> Next { get; set; }
    }

    public class CommandStorage<T> : Stack<T>, ICloneable
    {
        private StackItem<T> _head;
        public int ElementsCount;
        public new void Push(T value)
        {
            base.Push(value);
            _head = Peek();
            ElementsCount++;
        }

        public new T Pop()
        {
            var result = base.Pop();
            ElementsCount--;
            if (ElementsCount>0)
                _head = Peek();
            else
                _head = null;


        if (ElementsCount > 0)
            {
            }
            return result;
        }

        public object Clone()
        {
            return new CommandStorage<T>() { _head = _head, ElementsCount = ElementsCount };
        }
    }


    public class Clon : ICloneable
    {
        public CommandStorage<string> _clonModificationHistory { get; set; }
        public CommandStorage<string> _clonRollbackHistory { get; set; }
        public Clon()
        {
            _clonModificationHistory = new CommandStorage<string>();
            _clonRollbackHistory = new CommandStorage<string>();
        }

        public object Clone()
        {
            return new Clon
            {
                _clonModificationHistory = (CommandStorage<string>)_clonModificationHistory.Clone(),
                _clonRollbackHistory = (CommandStorage<string>)_clonRollbackHistory.Clone()
            };
        }
    }

    public class CommandExecutor
    {
        private Dictionary<string, Clon> _clonStorage;
        private Dictionary<string, Func<string>> _comandStorage;
        private string[] _parsedData;
        private string _clonNumber { get; set; }
        private string _programNumber { get; set; }
        public CommandExecutor(Dictionary<string, Clon> clonStorage)
        {
            _clonStorage = clonStorage;
            _comandStorage = new Dictionary<string, Func<string>>
            {
                ["learn"] = LearnCommand,
                ["rollback"] = RollbackCommand,
                ["relearn"] = RelearnCommand,
                ["clone"] = СloneCommand,
                ["check"] = CheckCommand
            };
        }

        public string Execute(string inputCommand)
        {
            _parsedData = inputCommand.Split();
            _clonNumber = _parsedData[1];
            return _comandStorage[_parsedData[0]]();
        }

        public string LearnCommand()
        {
            _programNumber = _parsedData[2];
            _clonStorage[_clonNumber]._clonModificationHistory.Push(_programNumber);
            return null;
        }

        public string RollbackCommand()
        {
            _programNumber = _clonStorage[_clonNumber]._clonModificationHistory.Pop();
            _clonStorage[_clonNumber]._clonRollbackHistory.Push(_programNumber);
            return null;
        }

        public string RelearnCommand()
        {
            _programNumber = _clonStorage[_clonNumber]._clonRollbackHistory.Pop();
            _parsedData = new string[] { "learn", "1", _programNumber };
            return LearnCommand();
        }

        public string СloneCommand()
        {
            _clonStorage[Convert.ToString(_clonStorage.Count + 1)] = (Clon)_clonStorage[_clonNumber].Clone();
            return null;
        }

        public string CheckCommand()
        {
            return _clonStorage[_clonNumber]._clonModificationHistory.Count > 0 ?
            _clonStorage[_clonNumber]._clonModificationHistory.Peek() :
                "basic";
        }
    }

    public class CloneVersionSystem : ICloneVersionSystem
    {
        private Dictionary<string, Clon> _clonStorage;
        private CommandExecutor _commandExecutor;

        public CloneVersionSystem()
        {
            _clonStorage = new Dictionary<string, Clon>() { { "1", new Clon() } };
            _commandExecutor = new CommandExecutor(_clonStorage);
        }

        public string Execute(string query)
        {
            return _commandExecutor.Execute(query);
        }
    }
}
