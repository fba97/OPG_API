# TODO OPG-APP

Crea l'infrastruttura per poter avviare la partita.
    - serve una classe tipo uow che possa utilizare per prendere ovunque da i progetti i dati riguardanti tutto.
    - classi da creare: partita, missione, componenti, aree, sottoaree?, partizioni, 

    come aggiungo il core? come un secondo servizio? 
            - l'api prende sempre da database e ci scrive (e se scrive mentre il core sta leggendo? gestione delle transazioni)
            - ogni volta che viene modificato il database il core deve saperlo e aggiornare le sue repository  




            ho deciso che siccome non deve essere un'applicazione real-time l'inizio dell'intero programma sta nell'api.
            e da li starto la dependency sui servizi, gli butto dentro la classe statica (plant), il data access...

            poi il core con i controller è una libreria di classi che viene utilizzata dall'api.

            il plant deve rimanere aggiornato ogni volta che viene chiamato un metodo dell'api.



-Crea il plant --> Game come classe statica.