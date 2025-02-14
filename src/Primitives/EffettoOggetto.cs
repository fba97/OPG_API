namespace Primitives
{
    public abstract class EffettoOggetto
    {
        public string Nome {  get; set; }
        public string Descrizione {  get; set; }
        public abstract Result<bool> CondizioniPerUso(Azione azione); // Result.Failure<bool>("Condizioni per l'uso non soddistatte");
        public abstract void Usa(Azione usaOggetto);
    }
}