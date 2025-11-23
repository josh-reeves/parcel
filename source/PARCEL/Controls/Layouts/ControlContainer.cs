using Microsoft.Maui.Layouts;

namespace PARCEL.Controls.Layouts;

public class ControlContainer : Layout
{
    protected override ILayoutManager CreateLayoutManager()
    {
        return new ControlContainerLayoutManager();
        
    }

    public class ControlContainerLayoutManager : ILayoutManager
    {
        public ControlContainerLayoutManager() {}

        Size ILayoutManager.Measure(double widthConstraint, double heightConstraint)
        {
            throw new NotImplementedException();
        }

        Size ILayoutManager.ArrangeChildren(Rect bounds)
        {
            throw new NotImplementedException();
        }
        
    }

}
