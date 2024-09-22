using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARCEL.Helpers;

public static class ViewBuilder<T>
{
    public delegate void AdditionalSetupDelegate();

    #region Methods
    public static T BuildView(T view, BindingPair[] bindings, AdditionalSetupDelegate? additionalSetup = null)
    {
        if (view is View)
            foreach (BindingPair binding in bindings)
                (view as View).SetBinding(binding.Property, binding.Path);

        additionalSetup?.Invoke();

        return view;

    }

    #endregion

    #region Structs
    public class BindingPair
    {
        public BindingPair(BindableProperty property, string path)
        {
            Property = property;
            Path = path;


        }

        public BindableProperty Property { get; private set; }
        public string Path { get; private set; }

    }

    #endregion

}
