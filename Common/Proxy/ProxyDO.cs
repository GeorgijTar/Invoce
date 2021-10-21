﻿using System;
using System.Windows;
using System.Windows.Data;


namespace Proxy
{
    /// <summary>Предоставляет прокси <see cref="DependencyObject"/> с одним свойством и 
    /// событем уведомляющем о его изменении.</summary>
    public class ProxyDO : DependencyObject
    {
        /// <summary>Свойство для задания внешних привязок.</summary>
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(object), typeof(ProxyDO), new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            ValueChanged?.Invoke(this, e);
        }

        /// <summary>Событие, возникающее при изменении значения любого <see cref="DependencyProperty"/>.</summary>
        public event EventHandler<DependencyPropertyChangedEventArgs> ValueChanged;

        /// <summary>Возвращает <see langword="true"/>, если значение свойства <see cref="Value"/> не задано.</summary>
        public bool IsUnsetValue => Equals(ReadLocalValue(ValueProperty), DependencyProperty.UnsetValue);

        /// <summary>Очистка всех <see cref="DependencyProperty"/> этого <see cref="ProxyDO"/>.</summary>
        public void Reset()
        {
            LocalValueEnumerator locallySetProperties = GetLocalValueEnumerator();
            while (locallySetProperties.MoveNext())
            {
                DependencyProperty propertyToClear = locallySetProperties.Current.Property;
                if (!propertyToClear.ReadOnly)
                {
                    ClearValue(propertyToClear);
                }
            }

        }

        /// <summary><see langword="true"/> если свойству задана Привязка.</summary>
        public bool IsValueBinding => BindingOperations.GetBindingExpressionBase(this, ValueProperty) != null;

        /// <summary><see langword="true"/> если свойству задана привязка
        /// и она в состоянии <see cref="BindingStatus.Active"/>.</summary>
        public bool IsActiveValueBinding
        {
            get
            {
                var exp = BindingOperations.GetBindingExpressionBase(this, ValueProperty);
                if (exp == null)
                    return false;
                var status = exp.Status;
                return status == BindingStatus.Active;
            }
        }

        public void SetValueBinding(BindingBase binding)
            => BindingOperations.SetBinding(this, ValueProperty, binding);
    }
}
