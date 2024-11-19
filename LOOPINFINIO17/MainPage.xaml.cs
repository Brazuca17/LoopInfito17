using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace LOOPINFINIO17;

public partial class MainPage : ContentPage
{
	Player player;

	public MainPage()
	{
		InitializeComponent();
		player = new Player(Imgplayer);
		player.Run();
	}

	bool estamorto = false;
	bool estaPulando = false;

	const int temporEntreFrames = 25;
		int velocidade1 = 0;
		int velocidade2 = 0;
		int velocidade3 = 0;
		int velocidade = 0;
		int larguraJanela = 0;
		int alturaJanela = 0;
		const int forcaGravidade = 6;
		bool estaNoChao = true;
		bool estaNoAr = false;
		int tempoPulando = 0;
		int tempoNoAr = 0;
		const int forcaPulo = 8;
		const int maxTempoPulando = 6;
		const int maxTempoAr = 4;

	void AplicaPulo()
	{
		estaNoChao = false;
		if (estaPulando && tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			estaNoAr = true;
			tempoNoAr = 0;
		}
		else if (estaNoAr && tempoNoAr >= maxTempoAr)
		{
			estaPulando = false;
			estaNoAr = false;
			tempoPulando = 0;
			tempoNoAr = 0;
		}	
		else if (estaPulando && tempoPulando < maxTempoPulando)
		{
			player.MoveY(-forcaPulo);
			tempoPulando++;
		}
		else if (estaNoAr)
		 tempoNoAr++;
	}



	void AplicaGravidade()
	{
		if(player.GetY()<0)
			player.MoveY(forcaGravidade);
			else if(player.GetY() >= 0)
			{
				player.SetY(0);
				estaNoChao = true;
				}
	}



	

	protected override void OnSizeAllocated(double  w, double h)
	{
		base.OnSizeAllocated(w, h);
		CorrigeTamanhoCenario(w, h);
		CalculaVelocidade(w);
	}

	void CalculaVelocidade(double w)
	{
		velocidade1 = (int)(w * 0.001);
		velocidade2 = (int)(w * 0.004);
		velocidade3 = (int)(w * 0.008);
		velocidade 	= (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(double w, double h)
	{
		foreach (var A in HSLayer1.Children)
			(A as Image).WidthRequest = w;
		
		foreach (var A in HSLayer2.Children)
			(A as Image).WidthRequest = w;

		foreach (var A in HSLayer3.Children)
			(A as Image).WidthRequest = w;
		
		foreach(var A in HSLayerChao.Children)
			(A as Image).WidthRequest = w;

		HSLayer1.WidthRequest = w;
		HSLayer2.WidthRequest = w;
		HSLayer3.WidthRequest = w;
		HSLayerChao.WidthRequest = w; 
	}

	void GerenciaCenario()
	{
		MoveCenario();
		GerenciaCenario(HSLayer1);
		GerenciaCenario(HSLayer2);
		GerenciaCenario(HSLayer3);
		GerenciaCenario(HSLayerChao);
	}

	void MoveCenario()
	{
		HSLayer1.TranslationX -= velocidade1;
		HSLayer2.TranslationX -= velocidade2;
		HSLayer3.TranslationX -= velocidade3;
		HSLayerChao.TranslationX -= velocidade;
	}

	void GerenciaCenario(HorizontalStackLayout HSL)
	{
		var view = (HSL.Children.First() as Image);
		if(view.WidthRequest + HSL.TranslationX < 0)
			{
				HSL.Children.Remove(view);
				HSL.Children.Add(view);
				HSL.TranslationX = view.TranslationX;
			}
	}


	async Task Desenha()
	{
		while(!estamorto)
		{
			GerenciaCenario();
			if(!estaPulando && !estaNoAr)
			{
				AplicaGravidade();
				player.Desenha();
			}
		 else 
		 AplicaPulo();
		 await Task.Delay(temporEntreFrames);
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	void Pulo(object o, TappedEventArgs a)
	{
		if(estaNoChao)
		{
			estaPulando	= true;
		}
	}
}