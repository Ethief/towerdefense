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

    // TODO: (JR) "Viholliset" muuttujan ei kannata olla enää luokkamuuttuja, kun 
    //  siinä on vain ihan viimeksi luotu vihollinen. Nimi on myös väärä,
    //  sillä täällä on vain yksi vihollinen-
    GameObject Viholliset;
    List<Label> valikonKohdat;
    List<Image> tasot;

    AssaultRifle perusase;
    List<int> wave; //lista vain luotu

    // TODO: (JR) Yksi tärkeimmistä asioista koodaamisessa on hyvä asioiden
    //   nimeäminen. Ei ei olleenkaan näin :)
    //  Pian teistä kukaan ei ole enää kärryillä mikä tämän muuttujan 
    //   tarkoitus saati sisältö on. Käyttäkää aina mahdollisimman kuvaavaa
    //   ja yksinkertaista muuttujan/aliohjelman nimeä.
    //  Sellainen ihan elämää helpottava vinkki muuten, että kun viet kursorin
    //   tuon "eaonparas" kohdalle, painat F2 ja annat uuden nimen, niin
    //   Visual C# vaihtaa nimeä kaikissa paikoissa missä "eaonparas". Näppärää.
    List<GameObject>eaonparas;

    //TODO LIST
    //KUN PAINAT NAPPIA VIHOLLISIA TULEE
    //vihollisa riittävä määrä
    //Toivottavasti joskus valmis =p
    //SAKU TEE PIKKUKUVA TASOSTA, JOTTA SEN VOI LAITTAA TASOVALIKKOON!!!
    //EI TOIMI VIELÄ EA SÄÄTELEE VALIKKOA JA EI TEXTUUREJA TASOLISTASSA!!!
    //ÄLÄ MUUTA KOODIA MITENKÄÄN!!!!!!!!!!! TV.VEISSULI
    //lisää turretteija

    public override void Begin()
    {
        IsMouseVisible = true;
        valikko();
        
        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
    // TODO: (JR) LuoViholliset -> LuoVihollinen, eli nimeäminen! nimeäminen!
    void LuoViholliset()
    {
        eaonparas = new List<GameObject>();

	// TODO: (JR) "Viholliset" -> "GameObject vihollinen", eli muuttuja 
        //  paikalliseksi ja parempi nimi. (muista F2-uudelleenimeämiskikka)
        Viholliset = new GameObject(50, 50);
        eaonparas.Add(Viholliset);
        Viholliset.Position = new Vector(-400,350);

        List<Vector> polku = new List<Vector>() {
            new Vector(-50,150),
            new Vector(-280, 1-80),
            new Vector(-130, -100),
            new Vector(53, 100),
            new Vector(0,290),
            new Vector(-70,290),
            new Vector(0,330),
            new Vector(220,330),
            new Vector(210,-110),
            new Vector(-150,-335),
            new Vector(300,-335)};
        PathFollowerBrain aivot = new PathFollowerBrain(2,polku);
        Viholliset.Brain = aivot;
        aivot.Active = true;

        //PiirraReittipisteet(polku);

        Viholliset.Image = LoadImage("VihollisenKuva");
        Viholliset.Shape = Shape.Circle;

        Add(Viholliset);

    }

    // TODO: (JR) Tämä kannattaa ottaa pois kommenteista. Riittää
    //  kun koodia ei kutsuta (ei sitä silloin ajata). Tätä 
    //  aliohjelmaa nimittäin tarvitaan taas kun lisäätte uusia
    //  karttoja. TOinen juttu. /* ... */ eli ton "/*" ja  "*/" välissä
    //  oleva on kommentti. Oli siinä sitten kuinka monta rivinvaihtoa tahansa.

    //private void PiirraReittipisteet(List<Vector> polku)
    
        //foreach (var wp in polku)
        
            //var tappa = new GameObject(10, 10);
            //tappa.Position = wp;
            //Add(tappa, 1);
        
    
    // TODO: (JR) Nimeäminen!, nimeen että mitä nämä ajastimet laskevat?
    Timer aikaLaskuri;
    Timer aikalaskuri2;
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
        //TASOVALIKKO EI VIELÄ TOIMI OTIN SEN POIS VÄLIAIKAISESTI, ETTÄ VOIDAAN TEHDÄ PELIÄ!!!
        //ClearAll();
        //Widget alusta = new Widget(new HorizontalLayout());
        //Add(alusta);
        //for (int i = 0; i < tasot.Count; i++)
        //{
            //Widget paikka = new Widget(tasot[i]);
            //alusta.BorderColor = Color.White;
            //alusta.Add(paikka);
        //}
        luotaso1();
        luomaali();
        luoperustorni();
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
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, takaisinvalikkoon, "");
    }

    void takaisinvalikkoon()
    {
        ClearAll();
        valikko();
    }
    void luomaali()
    {
        Vector paikka= new Vector (300,-335);
        PhysicsObject maali = new PhysicsObject(90,90);
        maali.Color = Color.DarkCyan;
        maali.Position=paikka;
        Add(maali);
        
    }

    bool ammuvihollisia(Vector range, Weapon ase)
    {
        if (eaonparas.Count > 0)
        {
            // TODO: (JR) Nimeäminen, mikä on muuttujassa?
            //  Lisäksi tässä pitäisi kai etsiä asetta lähin
            //  vihollinen? Pitää käyttää silmukkaa ja loopata
            //  kaikki viholliset läpi pitäen samalla jossain
            //  muuttujassa mikä oli se, jonka etäisyys 
            //  aseeseen oli lyhin (ja mikä tuo etäisyys oli).
            //  Vinkki: Vector.Distance(muuttuja.Position, ase.Position)
            GameObject muuttuja = eaonparas[0];

            // TODO: (JR) muuttuja.Angle on se suunta mihin muuttujassa oleva
            //  vihollinen osoittaa (eikä edes se mihin se on matkalla, 
            //  vaan se miten päin se on ruudulla. Siitä ei voi päätellä
            //  aseen suuntaa. Sen sijaan:
            // Vector nuoliVihollisestaAseeseen = muuttuja.Position-ase.Position:
            // ase.Angle = nuoliVihollisestaAseeseen.Angle;
            ase.Angle = muuttuja.Angle;

            // TODO: (JR) Tästä puuttuu vissin tarkastus, että onko vihu jo
            //  tarpeeksi lähellä. Voit käyttää if-ehtoa ja 
            //  nuoliVihollisestaAseeseen.Magnitude:a tähän.
       
            ase.Shoot();

            bool paluuarvo = true;
            return paluuarvo;
        }
        return false;
    }
    void luoperustorni()
    {
        // TODO: (JR) nimeäminen
        Vector juttu = new Vector(100,0);
        perusase = new AssaultRifle(35,10);
        // TODO: (JR) Hmm.. Tää FireRaten asettaminen kannattaa jättää pois.
        //  ihan sillä, että toi ajastin hoitaa jo tämän tulinopeuden.
        perusase.FireRate = 1;
        // TODO: (JR) nimeäminen!
        GameObject jokuli = new GameObject(40, 40);
        jokuli.Position = juttu;
        Add(jokuli);
        jokuli.Add(perusase);
        ajasta(juttu, perusase);
    }
    void ajasta(Vector range,Weapon ase)
    {
        aikalaskuri2 = new Timer();
        aikalaskuri2.Interval = 1.0;
        aikalaskuri2.Timeout += vvvkkk;
        aikalaskuri2.Start();

    }
    // TODO: (JR) NIMEÄMINEN!!!!
    void vvvkkk()
    {
        // TODO: (JR) NIMEÄMINEN NIMEÄMINEN NIMEÄMINEN :D
        Vector j = new Vector(0,0);
        ammuvihollisia(j, perusase);
    }

}


