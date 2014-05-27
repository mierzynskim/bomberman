using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bomberman.SettingsModel;
using Bomberman.StateInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

namespace Bomberman.StateImplementation
{
    /// <summary>
    /// Klasa odpowiedzialna za ładowanie zasobów gry (ustawień użytkownika itp.)
    /// </summary>
    public class LoadGameStorage: ILoadGameStorage
    {

        private readonly GameStorage storage;
        private StorageDevice device;
        private const string containerName = "MyGamesStorage";
        private const string filename = "gameStorage.sav";
        private StorageContainer container;
        /// <summary>
        /// Odczytuje obiekt GameStorage
        /// </summary>
        /// <returns></returns>
        public GameStorage Load()
        {
            if (!Guide.IsVisible)
            {
                device = null;
                var result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                result.AsyncWaitHandle.WaitOne();
                LoadFromDevice(result);
                result.AsyncWaitHandle.Close();
                var resultSerialized = Deserialize(container);
                if (resultSerialized == null)
                    return new GameStorage();
                return resultSerialized;
            }
            return null;
        }
        /// <summary>
        /// Metoda ładująca plik konfiguracji
        /// </summary>
        /// <param name="result">Resultat operacji</param>
        private void LoadFromDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();
        }
        /// <summary>
        /// Metoda pomocnicza deserializująca obiekt stanu gry
        /// </summary>
        /// <param name="container"></param>
        private static GameStorage Deserialize(StorageContainer container)
        {
            if (container.FileExists(filename))
            {
                Stream stream = container.OpenFile(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(GameStorage));
                var result = (GameStorage)serializer.Deserialize(stream);
                stream.Close();
                container.Dispose();
                return result;
            }
            return null;
        }
    }
}
