using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARCEL.Helpers;

public static class ViewBuilder<T>
{
    #region Methods
    public static T BuildView(T view, BindingPair[] bindings)
    {
        if (view is View)
            foreach (BindingPair binding in bindings)
                (view as View).SetBinding(binding.Property, binding.Path);

        return view;

    }

    #endregion

    #region Structs
    public class BindingPair(BindableProperty property, string path)
    {
        public BindableProperty Property { get; private set; } = property;
        public string Path { get; private set; } = path;

    }

    #endregion

}
