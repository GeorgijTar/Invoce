using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Proxy
{
    public class HeaderProxy : Freezable
    {
        protected override Freezable CreateInstanceCore() => new HeaderProxy();


        /// <summary>
        /// Название колонки
        /// </summary>
        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Title"/>.</summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(object), typeof(HeaderProxy), new PropertyMetadata(null));


        /// <summary>
        /// Коллекция для привязки
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="ItemsSource"/>.</summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(HeaderProxy), new PropertyMetadata(null));

    }

    public class HeaderProxyExtension : MarkupExtension
    {

        /// <summary>
        /// Название колонки
        /// </summary>
        public object Title { get; set; }

        /// <summary>
        /// Коллекция для привязки
        /// </summary>
        public BindingBase BindingItemsSource { get; set; }

        public HeaderProxyExtension()
        {
        }

        public HeaderProxyExtension(object title) => Title = title;

        public HeaderProxyExtension(object title, BindingBase bindingItemsSource)
            : this(title)
            => BindingItemsSource = bindingItemsSource;

        public HeaderProxyExtension(BindingBase bindingItemsSource, object title)
            : this(title, bindingItemsSource) { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var proxy = new HeaderProxy();
            proxy.Title = Title;
            if (BindingItemsSource != null)
                BindingOperations.SetBinding(proxy, HeaderProxy.ItemsSourceProperty, BindingItemsSource);
            return proxy;
        }
    }

}
