namespace OpenWpf.Helpers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Windows.Input;
    using System.Xml;

    public static class SaveFiles
    {
        public static T Deserialize<T>(string fileName)
        {
            DataContractSerializer serializer = new(typeof(T));
            using FileStream stream = File.OpenRead(fileName);
            return (T)serializer.ReadObject(stream);
        }

        public static void Serialize<T>(T obj, string fileName)
        {
            DataContractSerializer serializer = new(typeof(T));
            using XmlWriter writer = XmlWriter.Create(fileName);
            serializer.WriteObject(writer, obj);
        }
    }
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> execute, Predicate<object>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
    }
}