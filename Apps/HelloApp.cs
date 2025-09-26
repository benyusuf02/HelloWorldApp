namespace Helloworld.Apps;

[App(icon: Icons.PartyPopper, title: "Hello")]
public class HelloApp : ViewBase
{
    public override object? Build()
    {
        var nameState = this.UseState<string>("");

        // UI
        return Layout.Center()
               | new Card(
                   Layout.Vertical().Gap(6).Padding(3)
                   | Text.H2("Hello " + (string.IsNullOrEmpty(nameState.Value) ? "there" : nameState.Value) + "!")
                   | nameState.ToInput(placeholder: "What is your name? :))")
                 ).Width(Size.Units(200).Max(400));
                 
    }

    }