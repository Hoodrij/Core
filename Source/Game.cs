using Core;
using Core.StateMachine;
using Core.Ui;

public class Game
{
    public static Life Life { get; private set; }
    public static Assets Assets { get; private set; }
    public static Services Services { get; private set; }
    public static Models Models { get; private set; }
    public static UI UI { get; private set; }
    public static Fader Fader { get; private set; }

    public Game(IGameSetup setup)
    {
        Life = new Life();
        Assets = new Assets();
        Fader = new Fader();
        UI = new UI(setup.UIRoots());
        Models = new Models(setup.Models());
        Services = new Services(setup.Services());
    }

    public static void Reset()
    {
        Models.Reset();
        Services.Reset();
        UI.CloseAll();
        Fader.Reset();
    }
}