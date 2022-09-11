using System.Text.Json;

namespace Gallerio.Infrastructure.Services
{
    public abstract class BaseJsonFileDb<TDbModel>
    {
        private readonly string _dbPath;
        protected TDbModel DbModel { get; set; }

        private bool _dbLoaded = false;

        private SemaphoreSlim _dbLock = new SemaphoreSlim(1, 1);

        public BaseJsonFileDb(string dbPath, TDbModel emptyModel)
        {
            _dbPath = dbPath;
            DbModel = emptyModel;
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
            if (!File.Exists(_dbPath))
            {
                _dbLoaded = true;
                return;
            }
                
            using (FileStream stream = new FileStream(_dbPath, FileMode.Open))
            {
                var res = await JsonSerializer.DeserializeAsync<TDbModel>(stream);
                if (res is not null)
                    DbModel = res;
            }

            _dbLoaded = true;
        }

        internal async Task PersistChangesToFile()
        {
            await _dbLock.WaitAsync();
            try
            {
                using (FileStream stream = File.Create(_dbPath))
                {
                    await JsonSerializer.SerializeAsync(stream, DbModel);
                }
            }
            finally
            {
                _dbLock.Release();
            }
        }

        internal async Task<TDbModel> GetModel()
        {
            await LoadDbIfNotLoaded();
            return DbModel;
        }
    }
}
