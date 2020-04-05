using Core;
using Core.Ui;

public class Game
{
    public static Life Life { get; private set; }
    public static Assets Assets { get; private set; }
    public static Services Services { get; private set; }
    public static Models Models { get; private set; }
    public static UI UI { get; private set; }
    public static Fader Fader { get; private set; }

    public Game()
    {
        Life = new Life();
        Assets = new Assets();
        Services = new Services();
        Models = new Models();
        UI = new UI();
        Fader = new Fader();
    }

    public void Setup(IGameSetup setup)
    {
        UI.Add(setup.UIRoots());
        Models.Add(setup.Models());
        Services.Add(setup.Services());
    }

    public static void Reset()
    {
        Models.Reset();
        Services.Reset();
        UI.CloseAll();
        Fader.Reset();
    }
}