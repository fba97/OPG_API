using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection.Metadata.Ecma335;
using Primitives;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Game
{
    public partial class Game
    {

        private readonly IServiceProvider _services;
        private readonly IConfiguration _cfg;
        private readonly string _connString;
        private readonly ILogger<Game> _log;

        public Mappa Mappa { get => _mappa; }
        private readonly Mappa _mappa;

        public IEnumerable<PersonaggioInPartita> PersonaggioInPartita { get => _personaggioInPartita; }
        private readonly PersonaggioInPartita[] _personaggioInPartita;

        public IEnumerable<Oggetto> _OggettiiInPartita { get => _oggettiiInPartita; }
        private readonly Oggetto[] _oggettiiInPartita;





        public Game
            (
                IConfiguration configuration,
                ILogger<Game> logger,
                IServiceProvider services
            )
        {
            _cfg = configuration;
            _log = logger;
            _services = services;


            _log.LogInformation($"Initiating Game bootstrapping process");
            var t0 = DateTime.Now;
            try
            {

                _connString = _cfg.GetConnectionString("sqlStringConnection");


                _mappa = GetMappa();
                _subItems = GetAllSubItems().ToArray();


                _log.LogInformation($"Proceeding with Item bootstrapping {(DateTime.Now - t0).TotalMilliseconds} ms");

                _items = GetAllItems().ToArray();
                _subAreas = GetAllSubAreas().ToArray();
                _areas = GetAllAreas().ToArray();
                var warehouses = GetAllWarehouses().ToArray();
                foreach (var warehouse in warehouses)
                    warehouse.LocatedIn = this;
                _warehouses = warehouses;

                _log.LogInformation($"Warehouse fully loaded {(DateTime.Now - t0).TotalMilliseconds} ms, getting links...");


                var links = BuildLinks().ToArray();

                _log.LogInformation($"Active links loaded {(DateTime.Now - t0).TotalMilliseconds} ms, getting link groups...");
                var ruleProvider = _services.GetRequiredService<IGroupingRuleProvider>();

                _linkGroups = GetGroupedLinks(ruleProvider, links).ToArray();


                _log.LogInformation($"Link Groups loaded {(DateTime.Now - t0).TotalMilliseconds} ms ...");
                _links = ProcessLinks(links).ToArray();
                LinkLoadingUnitTypeExclusion = RepositoryManager.LinkRepository.GetLinkLoadingUnitTypeExclusions();
                LinkMissionTypeExclusion = RepositoryManager.LinkRepository.GetLinkLinkMissionTypeExclusions();
                _activeLinks = _links.Where(a => a.Enabled).ToArray();



                _recalc = RepositoryManager.PartitionRepository.GetRecomputationPartitions().ToArray();
                _redirects = RepositoryManager.PartitionRepository.GetRedirectPartitions().ToArray();

                CompoundLinks = GetCompoundLinks().ToArray();

                var lines = RepositoryManager.PartitionRepository.GetLines().ToArray();

                Lines = BuildLines(lines).ToArray();

                _log.LogInformation($"Lines correctly bootstrapped, proceeding with stock {(DateTime.Now - t0).TotalMilliseconds} ms");

                _stock = GetStock(_luTypes).ToList();
                _stock.ForEach(s => s.CurrentPartition.AddLoadingUnit(s));

                _log.LogInformation($"Stock completed, proceeding with missions {(DateTime.Now - t0).TotalMilliseconds} ms");

                SentMessagesTracker = sentMessagesTracker;

                _loadingUnitDeletionActions = new Lazy<IEnumerable<ILoadingUnitDeletionAction>>
                (
                    () => services.GetServices<ILoadingUnitDeletionAction>()
                );

                _loadingUnitDeletionHandlers = new Lazy<IEnumerable<ILoadingUnitDeletionHandler>>
                (
                    () => services.GetServices<ILoadingUnitDeletionHandler>()
                );
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex.ToString());
                throw;
            }


            _log.LogInformation("Successful Plant Bootstrap {0} ms", (DateTime.Now - t0).TotalMilliseconds);
        }







    }
}
