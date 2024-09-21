namespace PARCEL.TriggerActions;

public class AddControlToLayoutTriggerAction : TriggerAction<Layout>
{
    private IView control;

    public AddControlToLayoutTriggerAction(IView controlToAdd)
    {
        control = controlToAdd;

    }

    protected override void Invoke(Layout sender)
    {
        sender.Add(control);

        Console.WriteLine("Added control");
    }

}
