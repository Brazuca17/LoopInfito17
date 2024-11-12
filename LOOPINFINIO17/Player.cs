using FFImageLoading.Maui;

namespace LOOPINFINIO17;

public delegate void Callback();

public class Player:Animacao
{
    public Player (CachedImageView a) : base (a)
    {
        for (int i = 1; i < 24; ++i)
            animacao1.Add($"player{i.ToString("D2")}.png");
        for (int i = 1; i < 27; ++i)
            animacao2.Add($"dead{i.ToString("D2")}.png");
        SetAnimacaoAtiva(1);    
    }

    public void Die()
    {
        loop = false;
        SetAnimacaoAtiva(2);
    }


    public void Run()
    {
        loop=true;
        SetAnimacaoAtiva(1);
        Play();
    }






}