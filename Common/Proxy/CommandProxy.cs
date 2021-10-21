using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Proxy
{
    /// <summary>Предоставляет прокси <see cref="Freezable"/> для привязки команды.</summary>
    public class CommandProxy : Freezable, ICommand
    {
        #region Command Property
        /// <summary>Экземпляр команды.</summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Command"/>.</summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CommandProxy), new PropertyMetadata(null, CommandChanged));

        // Метод вызываемый при изменении свойства Command.
        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandProxy proxy = (CommandProxy)d;
            if (proxy.privateCommand != null)
            {
                proxy.privateCommand.CanExecuteChanged -= proxy.RaiseCanExecuteChanged;
            }

            proxy.privateCommand = (ICommand)e.NewValue;
            if (proxy.privateCommand != null)
            {
                proxy.privateCommand.CanExecuteChanged += proxy.RaiseCanExecuteChanged;
            }

            proxy.RaiseCanExecuteChanged();
        }

        /// <summary>Метод поднимающий событие <see cref="CanExecuteChanged"/>.</summary>
        /// <param name="sender">Игнорируется.</param>
        /// <param name="e">Игнорируется.</param>
        /// <remarks>Эта перегрузка метода <see cref="RaiseCanExecuteChanged()"/> создана 
        /// для удобства полключения прослушки к событиям <see cref="Command"/> и <see cref="CommandManager"/>.</remarks>
        protected void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        // Приватное поле хранящее значение DependecyProperty CommandProperty.
        // Объявленно для уменьшения обращений к CommandProperty,
        // так как это довольно затратно.
        private ICommand privateCommand;

        #endregion

        #region ICommand
        /// <inheritdoc cref="ICommand.CanExecuteChanged"/>
        public event EventHandler CanExecuteChanged;

        /// <summary>Поднимает событие <see cref="CanExecuteChanged"/>.</summary>
        /// <remarks>Если метод вызывается не в потоке <see cref="DispatcherObject.Dispatcher"/>,
        /// то производится маршалинг в этот поток.</remarks>
        public void RaiseCanExecuteChanged()
        {
            if (Dispatcher.CheckAccess())
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _ = Dispatcher.BeginInvoke(raiseCanExecuteChanged);
            }
        }
        private readonly Action raiseCanExecuteChanged;

        /// <inheritdoc cref="ICommand.CanExecute"/>
        public bool CanExecute(object parameter)
        {
            return privateCommand?.CanExecute(parameter) ?? true;
        }

        /// <inheritdoc cref="ICommand.Execute"/>
        public void Execute(object parameter)
        {
            privateCommand?.Execute(parameter);
        }
        #endregion

        #region  Freezable
        /// <summary><inheritdoc cref="Freezable.CreateInstanceCore"/></summary>
        /// <returns><inheritdoc cref="Freezable.CreateInstanceCore"/></returns>
        /// <remarks>Не реализованно.</remarks>
        /// <exception cref="NotImplementedException">При любом обращении.</exception>
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Listening CommandManager.RequerySuggested

        // Делегат для подписки на CommandManager.RequerySuggested
        private readonly EventHandler requerySuggested;

        /// <summary>Инициализирует экземпляр <see cref="CommandProxy"/>.</summary>
        public CommandProxy()
        {
            requerySuggested = RaiseCanExecuteChanged;
            raiseCanExecuteChanged = () => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Прослушка <see cref="CommandManager.RequerySuggested"/>.</summary>
        /// <remarks>По умолчанию событие не прослушивается.
        /// Необходимо включить, если в экземпляре команды, переданном в свойство <see cref="Command"/>,
        /// не реализованная прослушка, а по логике GUI она необходима.</remarks>
        public bool IsListenRequerySuggested
        {
            get => (bool)GetValue(IsListenRequerySuggestedProperty);
            set => SetValue(IsListenRequerySuggestedProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="IsListenRequerySuggested"/>.</summary>
        public static readonly DependencyProperty IsListenRequerySuggestedProperty =
            DependencyProperty.Register(nameof(IsListenRequerySuggested), typeof(bool), typeof(CommandProxy), new PropertyMetadata(false, IsListenRequerySuggestedChanged));

        // Метод включает/отключает прослушку при изменении значения свойства IsListenRequerySuggested.
        private static void IsListenRequerySuggestedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CommandProxy proxy = (CommandProxy)d;
            if ((bool)e.NewValue)
            {
                CommandManager.RequerySuggested += proxy.requerySuggested;
            }
            else
            {
                CommandManager.RequerySuggested -= proxy.requerySuggested;
            }
        }


        #endregion
    }
}
