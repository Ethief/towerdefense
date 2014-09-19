using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class TW : PhysicsGame
{
    
    GameObject Viholliset; 
    //TODO LIST
    //KUN PAINAT NAPPIA VIHOLLISIA TULEE
    //SAKU NE GRAFFAT  
    //laita viholliset liikkumaan.
    public override void Begin()
    {
        LuoViholliset();
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    void LuoViholliset()
    {
        Viholliset = new GameObject(20, 20);
   
        List<Vector> polku = new List<Vector>();
        polku.Add( new Vector(-50, -100) );
        polku.Add( new Vector(-100, 50)  );
        polku.Add( new Vector(-250, -200));
        
        PathFollowerBrain aivot = new PathFollowerBrain(100, polku);
        Viholliset.Brain = aivot;
        aivot.Active = true;
        
        //Viholliset.Image = 
        Viholliset.Shape = Shape.Circle;
        
        Add(Viholliset); 
        
    }
}

