namespace LOOPINFINIO17;
using FFImageLoading.Maui;

public class Inimigos
{
    List<Inimigo> inimigos = new List<Inimigo>();
    Inimigo atual = null;
    double minX = 0;
    public Inimigos(double a)
    {
        minX = a;
    }

    public void Add(Inimigo a)
    {
        inimigos.Add(a);
        if(atual == null)
           atual = a;
        Iniciar();
    }

    public void Iniciar()
    {
        foreach(var e in inimigos)
        e.Reset();
    }

    void Gerencia()
    {
        if(atual.GetX() < minX)
        {
            Iniciar();
            var r = Random.Shared.Next(0, inimigos.Count);
            atual = inimigos[r];
        }
    }

    public void Desenha(int Veloc)
    {
        atual.MoveX(Veloc);
        Gerencia();
    }
}