using Microsoft.Maui.Dispatching;

namespace FindByNameInGrid;

public partial class MainPage : ContentPage
{
	int count = 0;
	int numeroBottoni = 9;
	int secondi = 5;
	IDispatcherTimer timer;


    public MainPage()
	{
		InitializeComponent();
		DisabilitaBottoni();
	}


    void Btn_Clicked(System.Object sender, System.EventArgs e)
    {
        var b = sender as ImageButton;

		//Blocca reclick
        if (b.Source.ToString() != "arancio.png")
		{
			b.IsEnabled = false;
			b.Source = "arancio.png";
			count++;
		}
		
		if (count >= 3)
			DisabilitaBottoniPerNome();

    }


	void DisabilitaBottoni()
	{
		foreach (var element in Griglia.Children)
		{
			var b = element as ImageButton;
            //Sintassi equivalente:
            //var b = (ImageButton)element:
            b.IsEnabled = false;
		}
	}

    void AbilitaBottoni()
    {
        foreach (var element in Griglia.Children)
        {
            var b = element as ImageButton;
            //Sintassi equivalente:
            //var b = (ImageButton)element:
            b.IsEnabled = true;
        }
    }

    void DisabilitaBottoniPerNome()
	{
        string nomeBottone = "";
		for (int i = 0; i < numeroBottoni; i++)
		{
			nomeBottone = $"Btn{i}";
			ImageButton b = Griglia.FindByName(nomeBottone) as ImageButton;
            b.IsEnabled = false;
        }
    }

	void GestioneTemporizzazioni(int intervallo)
	{
		//Crea un temporizzatore. Per potere avere visibilità del timer da tutta
		//la classe, è bene dichiarare la variabile timer fuori dal metodo nella
		//parte alta della MainPage
		timer = Dispatcher.CreateTimer();

		//Imposta l'intervallo. L'intervallo va fornito come TimeSpan
		//ed è possibile utilizzare unità diverse dai millisecondi
		//utilizzando i vari metodi FromSeconds, FromMinutes, FromHours, ecc.
		timer.Interval = TimeSpan.FromMilliseconds(intervallo);

		//Quando il timer scade, e ciò avverà allo scadere di ogni intervallo
		//verrà generato un evento a cui andrà associato il relativo event
		//handler, analogamente a quello che facciamo quando associamo un
		//event handler ad un bottone.
		//Si fa così:
		timer.Tick += TimerHandler;

		//Il timer non è ancora operativo. Per avviarlo occorre richiamare il
		//metodo Start() e per arrestarlo occorre richiamare il metodo Stop().
		//Una volta avviato, il timer proseguirà il proprio lavoro in maniera
		//indipendente dal programma e non dovremo preoccuparcene più.
		//È bene però precisare che il timer viene eseguito all'interno del
		//thread della UI, per cui il codice dell'event handler del timer non
		//deve essere bloccante, perché, in caso contrario, si bloccherebbe
		//anche la UI.
		//
		//È bene non lasciare acceso il timer se non serve. Questi i comandi
		//utili:
		//
		//Fermare il timer
		//timer.Stop();
		//
		//Rimuovere l'event handler
		//timer.Tick -= TimerHandler;
		//
		//Deallocare il timer
		//timer = null;
		
    }

    private void TimerHandler(object sender, EventArgs e)
    {
		//Codice da eseguire ogni volta che il timer scatta
		//Questo esempio blocca l'applicazione dopo 5 secondi
		//secondi è: int secondi = 5;
		secondi--;

		//Visualizza il tempo ancora a disposizione
		AreaMessaggi.Text = secondi.ToString();
		if (secondi < 0)
		{
			timer.Stop();
			timer = null;
			AreaMessaggi.Text = "Game Over";
			DisabilitaBottoni();
		}
		
    }

    void btnGioca_Clicked(System.Object sender, System.EventArgs e)
    {
		btnGioca.IsEnabled = false;
		GestioneTemporizzazioni(1000);
		AbilitaBottoni();
        timer.Start();
    }
}


