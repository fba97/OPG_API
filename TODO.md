# TODO OPG-APP

1 - Crea l'infrastruttura per poter avviare la partita.
        - partitaRepository: - CreaPartita() ok
                             - GetPartita(id) ok
                             - updatePartita(Partita) cosa updata? il nome? personaggi e oggetti son gia salvati.
                             - deletePartita ok
        - partitaController:    - CreaPartita ok
                                - LoadPartita 
                                - getAllPartite ok
                                
salavataggio e load: https://www.youtube.com/watch?v=f5GvfZfy3yk&ab_channel=DapperDino
loaddare una partita significa:
cancellare tutte le info che hai nelle tabelle attuali
 e riempirlo con tutte le informazioni CHE HAI RELATIVE A QUELLA PARTITA

 ci vuole quindi prima di tutto un metodo che riempie casualmente quelle tabelle 
 e poi uno che prendendo dalle tabelle salvataggio i dati li copia nelle tabelle attuali 

 per creare gli oggetti ha senso usare un factory oppure basta che faccio dei cicli guardando oggetti, personaggi, carte? 
 che vantaggi ho nel creare una factory?
 su l2-software-solution per creare gli item siccome hanno tutti una certa serie di parametri (id, subAreaId, code, description, subItems, type, communicationChannelId)
 estendono tutti item 

potrei creare un factory che estende un oggetto item (con id, nome, tipo, descrizione, posizionex, posizione y, idpartita,stato)



creazione degli oggetti, personaggi, creazione partita -> in base alle impostazioni che vengono inserite 



salvataggio e load della sessione:
in una tabella salvataggi: idSalvataggio, data, idPartita, json: 




da risistemare il personaggio_base e Personaggio_in_partita. i personaggi sono sempre gli stessi. sempre base. come gli oggetti. poi nelle partite salvate (un file json dentro ad una colonna di una tabela ) andrò a mettere i valori attuali nella partita.