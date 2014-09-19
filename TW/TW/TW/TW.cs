using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class TW : PhysicsGame
{
    Image taustaKuva = LoadImage("kentta");
    GameObject Viholliset;
    //TODO LIST
    //KUN PAINAT NAPPIA VIHOLLISIA TULEE
    //vihollisa riittävä määrä
    //Toivottavasti joskus valmis =p


    public override void Begin()
    {
        LuoAikaLaskuri();
        Level.Background.Image = taustaKuva;
        Level.Background.FitToLevel();
        SmoothTextures = false;
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LuoViholliset()
    {
        Viholliset = new GameObject(50, 50);
        Viholliset.Position = new Vector(-400,350);

        List<Vector> polku = new List<Vector>() {
            new Vector(-50,150),
            new Vector(-280, 1-80),
            new Vector(-130, -100),
            new Vector(53, 100),
            new Vector(0,290)};
        PathFollowerBrain aivot = new PathFollowerBrain(3, polku);
        Viholliset.Brain = aivot;
        aivot.Active = true;

        PiirraReittipisteet(polku);

        Viholliset.Image = LoadImage("VihollisenKuva");
        Viholliset.Shape = Shape.Circle;

        Add(Viholliset);

    }

    private void PiirraReittipisteet(List<Vector> polku)
    {
        foreach (var wp in polku)
        {
            var tappa = new GameObject(10, 10);
            tappa.Position = wp;
            Add(tappa, 1);
        }
    }

    Timer aikaLaskuri;

    void LuoAikaLaskuri()
    {


        aikaLaskuri = new Timer();
        aikaLaskuri.Interval = 1.0;
        aikaLaskuri.Timeout += LuoViholliset;
        aikaLaskuri.Start();


    }







}

