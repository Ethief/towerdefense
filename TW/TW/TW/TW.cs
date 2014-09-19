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
        
        Level.Background.Image = taustaKuva;
        Level.Background.FitToLevel();
        SmoothTextures = false;
        LuoViholliset();
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LuoViholliset()
    {
        Viholliset = new GameObject(50, 50);

        List<Vector> polku = new List<Vector>() {
            new Vector(50, -100),
            new Vector(-100, 50),
            new Vector(-250, -200)};

        PathFollowerBrain aivot = new PathFollowerBrain(3, polku);
        Viholliset.Brain = aivot;
        aivot.Active = true; 
        

        Viholliset.Image = LoadImage("VihollisenKuva");
        Viholliset.Shape = Shape.Circle;
        
        Add(Viholliset); 
        
    }
    DoubleMeter alaspainLaskuri;
   Timer aikaLaskuri;

    void LuoAikaLaskuri()
{
    alaspainLaskuri = new DoubleMeter(30);
    
    aikaLaskuri = new Timer();
    aikaLaskuri.Interval = 0.1;
    aikaLaskuri.Timeout += LaskeAlaspain;
    aikaLaskuri.Start();

    Label aikaNaytto = new Label();
    aikaNaytto.TextColor = Color.White;
    aikaNaytto.DecimalPlaces = 1;
    aikaNaytto.BindTo(alaspainLaskuri);
    Add(aikaNaytto);
}

    void LaskeAlaspain()
    {
    alaspainLaskuri.Value -= 0.1;

    if (alaspainLaskuri.Value <= 0)
   
        MessageDisplay.Add("Aika loppui...");
        aikaLaskuri.Stop();
    }
        
    

    
    
  }

