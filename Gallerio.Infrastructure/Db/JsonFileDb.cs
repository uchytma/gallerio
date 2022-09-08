using Gallerio.Api.Options;
using Gallerio.Infrastructure.Options;
using Gallerio.Infrastructure.Services.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gallerio.Infrastructure.Db
{
    public class JsonFileDb
    {
        private readonly string _dbPath;

        private DbModel _dbModel = DbModel.Empty;

        private bool _dbLoaded = false;

        private SemaphoreSlim _dbLock = new SemaphoreSlim(1, 1);

        public JsonFileDb(IOptions<ApplicationOptions> applicationOptions)
        {
            var opt = applicationOptions.Value ?? throw new OptionsNotFoundException(typeof(ApplicationOptions));
            _dbPath = opt.DatabasePath ?? throw new OptionsMemberNotFoundException(typeof(ApplicationOptions), nameof(opt.DatabasePath));

        }

        internal async Task LoadDbIfNotLoaded()
        {
            if (!_dbLoaded)
            {
                await _dbLock.WaitAsync();
                try
                {
                    if (!_dbLoaded)
                        await ReloadDataFromJsonFile();
                }
                finally
                {
                    _dbLock.Release();
                }
            }
        }

        internal async Task ReloadDataFromJsonFile()
        {
            if (!File.Exists(_dbPath)) return;

            using (FileStream stream = new FileStream(_dbPath, FileMode.Open))
            {
                var res = await JsonSerializer.DeserializeAsync<DbModel>(stream);
                if (res is not null)
                    _dbModel = res;
            }

            _dbLoaded = true;
        }

        internal async Task<DbModel> GetModel() 
        {
            await LoadDbIfNotLoaded();
            return _dbModel;
        }

        internal async Task PersistChangesToFile()
        {
            await _dbLock.WaitAsync();
            try
            {
                if (!File.Exists(_dbPath)) File.Create(_dbPath);
                using (FileStream stream = new FileStream(_dbPath, FileMode.Truncate))
                {
                   await JsonSerializer.SerializeAsync<DbModel>(stream, _dbModel);
                }
            }
            finally
            {
                _dbLock.Release();
            }
        }
    }
}
