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
    List<Label> valikonKohdat;
    List<Image> tasot;
    //TODO LIST
    //KUN PAINAT NAPPIA VIHOLLISIA TULEE
    //vihollisa riittävä määrä
    //Toivottavasti joskus valmis =p
    //SAKU TEE PIKKUKUVA TASOSTA, JOTTA SEN VOI LAITTAA TASOVALIKKOON!!!
    //EI TOIMI VIELÄ EA SÄÄTELEE VALIKKOA JA EI TEXTUUREJA TASOLISTASSA!!!
    //ÄLÄ MUUTA KOODIA MITENKÄÄN!!!!!!!!!!! TV.VEISSULI


    public override void Begin()
    {
        IsMouseVisible = true;
        valikko();
        
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
    void valikko()
    {
        valikonKohdat = new List<Label>();
        Label kohta1 = new Label("play mode");
        kohta1.Position = new Vector(0, 40);
        valikonKohdat.Add(kohta1);
        Label kohta2 = new Label("Arcade mode");
        kohta2.Position = new Vector(0, 0);
        valikonKohdat.Add(kohta2);
        Label kohta3 = new Label("options");
        kohta3.Position = new Vector(0, -40);
        valikonKohdat.Add(kohta3);
        Label kohta4 = new Label("Quit");
        kohta4.Position = new Vector(0, -80);
        valikonKohdat.Add(kohta4);
        foreach (Label valikonKohta in valikonKohdat)
        {
            Add(valikonKohta);
        }
        Mouse.ListenOn(kohta1, MouseButton.Left, ButtonState.Pressed, playmode, null);
        Mouse.ListenOn(kohta2, MouseButton.Left, ButtonState.Pressed, arcademode, null);
        Mouse.ListenOn(kohta3, MouseButton.Left, ButtonState.Pressed, options, null);
        Mouse.ListenOn(kohta4, MouseButton.Left, ButtonState.Pressed, Exit, null);
        Mouse.ListenMovement(1.0, ValikossaLiikkuminen, null);
    }
    void playmode()
    {
        ClearAll();
        Widget alusta = new Widget(new HorizontalLayout());
        Add(alusta);
        for (int i = 0; i < tasot.Count; i++)
        {
            Widget paikka = new Widget(tasot[i]);
            alusta.BorderColor = Color.White;
            alusta.Add(paikka);
        }
        

    }
    void arcademode()
    {

    }
    void options()
    {
    }
    void ValikossaLiikkuminen(AnalogState hiirenTila)
    {
        foreach (Label kohta in valikonKohdat)
        {
            if (Mouse.IsCursorOn(kohta))
            {
                kohta.TextColor = Color.Red;
            }
            else
            {
                kohta.TextColor = Color.Black;
            }

        }
    }

    void luotaso1()
    {
        ClearAll();
        LuoAikaLaskuri();
        Level.Background.Image = taustaKuva;
        Level.Background.FitToLevel();
        SmoothTextures = false;
    }





}

