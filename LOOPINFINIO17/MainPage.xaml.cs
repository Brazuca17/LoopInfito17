namespace LOOPINFINIO17;

public partial class MainPage : ContentPage
{


	public MainPage()
	{
		InitializeComponent();
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
			await Task.Delay(temporEntreFrames);
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
}