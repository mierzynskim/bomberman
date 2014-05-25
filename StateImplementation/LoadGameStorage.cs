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
    public class LoadGameStorage: ILoadGameStorage
    {

        private readonly GameStorage storage;
        private StorageDevice device;
        private const string containerName = "MyGamesStorage";
        private const string filename = "gameStorage.sav";
        private StorageContainer container;

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

        void LoadFromDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();
        }

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
