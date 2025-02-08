using Primitives;

namespace Core.Game_dir
{
    public partial class Game
    {
        private OggettoTemplate[] _oggettiTemplate;

        // Questo metodo va chiamato in InitGeneralInfo dopo aver caricato AllOggetti
        private void InitOggettiTemplate()
        {
            _oggettiTemplate = AllOggetti.Select(oggetto => new OggettoTemplate
            {
                Id = oggetto.Id,
                Nome = oggetto.Nome,
                Descrizione = oggetto.Descrizione,
                Tipo = oggetto.Tipo,
                Stato = oggetto.Stato,
                BonusAttacco = oggetto.BonusAttacco,
                BonusDifesa = oggetto.BonusDifesa
            }).ToArray();
        }

        private IEnumerable<Inventario> InitInventari(List<int> personaggiIds)
        {
            var inventari = new List<Inventario>();
            var oggettiUsatiLocalmente = new HashSet<int>();

            // Prima inizializziamo la mappa
            var oggettiMappa = InitOggettiMappa();
            inventari.Add(new Inventario(id: 0, idPersonaggio: null, oggettiMappa) { Tipo = TipoInventario.Mappa });

            // Aggiungiamo gli ID degli oggetti della mappa al nostro tracking locale
            foreach (var item in oggettiMappa)
            {
                oggettiUsatiLocalmente.Add(item.Oggetto.Id);
            }

            // Passiamo gli oggetti già usati al metodo del negozio
            var oggettiNegozio = InitOggettiNegozio(oggettiUsatiLocalmente);
            inventari.Add(new Inventario(id: 1, idPersonaggio: null, oggettiNegozio) { Tipo = TipoInventario.Negozio });

            var countInventari = inventari.Count();
            // Aggiungi gli inventari dei personaggi partendo da id = 2
            inventari.AddRange(personaggiIds.Select((id, index) =>
                new Inventario(countInventari + index, id) { Tipo = TipoInventario.Personaggio }));

            return inventari;
        }

        // Metodo helper per creare nuove istanze di oggetti
        private Oggetto CreaIstanzaDaTemplate(OggettoTemplate template, int? posizione = null)
        {
            return new Oggetto(
                id: template.Id,
                nome: template.Nome,
                descrizione: template.Descrizione,
                tipo: (int)template.Tipo,
                stato: (int)template.Stato,
                bonusAttacco: template.BonusAttacco,
                bonusDifesa: template.BonusDifesa,
                id_Posizione: posizione
            );
        }

        private List<OggettoInventario> InitOggettiMappa()
        {
            var random = new Random();
            var oggettiUsati = GetOggettiUsati();
            var posizioniDisponibili = new List<int> { 10, 15, 20, 25, 30 };
            var oggettiMappa = new List<OggettoInventario>();

            foreach (var posizione in posizioniDisponibili)
            {
                var templateDisponibile = _oggettiTemplate
                    .Where(t => !oggettiUsati.Contains(t.Id))
                    .OrderBy(x => random.Next())
                    .FirstOrDefault();

                if (templateDisponibile == null) break;

                oggettiUsati.Add(templateDisponibile.Id);
                var nuovoOggetto = CreaIstanzaDaTemplate(templateDisponibile, posizione);

                oggettiMappa.Add(new OggettoInventario
                {
                    Oggetto = nuovoOggetto,
                    Quantita = 1,
                    IsEquipaggiato = false
                });
            }

            return oggettiMappa;
        }
        private List<OggettoInventario> InitOggettiNegozio(HashSet<int> oggettiUsatiLocalmente)
        {
            var random = new Random();
            // Combiniamo gli oggetti usati dalla partita con quelli usati localmente
            var oggettiUsati = GetOggettiUsati();
            oggettiUsati.UnionWith(oggettiUsatiLocalmente);

            var oggettiNegozio = new List<OggettoInventario>();
            var templatePerNegozio = _oggettiTemplate
                .Where(t => !oggettiUsati.Contains(t.Id))
                .Where(t => t.Stato == StatoOggetto.Nuovo)
                .OrderBy(x => random.Next())
                .Take(5);

            foreach (var template in templatePerNegozio)
            {
                var nuovoOggetto = CreaIstanzaDaTemplate(template);
                oggettiNegozio.Add(new OggettoInventario
                {
                    Oggetto = nuovoOggetto,
                    Quantita = 1,
                    IsEquipaggiato = false
                });
            }

            return oggettiNegozio;
        }

        private HashSet<int> GetOggettiUsati()
        {
            var oggettiUsati = new HashSet<int>();

            if (_partita?.Inventari == null) return oggettiUsati;

            foreach (var inventario in _partita.Inventari)
            {
                foreach (var item in inventario.Oggetti)
                {
                    oggettiUsati.Add(item.Oggetto.Id);
                }
            }

            return oggettiUsati;
        }
    }
}