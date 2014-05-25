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
    public class SaveGameStorage : ISaveGameStorage
    {
        private StorageDevice device;
        private const string ContainerName = "MyGamesStorage";
        private const string Filename = "gameStorage.sav";
        private StorageContainer container;


        public void Save(GameStorage gameStorage)
        {
            if (!Guide.IsVisible)
            {
                device = null;
                var result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                result.AsyncWaitHandle.WaitOne();
                LoadFromDevice(result);
                result.AsyncWaitHandle.Close();
                Serialize(container, gameStorage);
            }
        }

        void LoadFromDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer(ContainerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();
        }

        private static void Serialize(StorageContainer container, GameStorage gameStorage)
        {
            if (container.FileExists(Filename))
                container.DeleteFile(Filename);
            Stream stream = container.CreateFile(Filename);

            XmlSerializer serializer = new XmlSerializer(typeof(GameStorage));
            serializer.Serialize(stream, gameStorage);
            stream.Close();
            container.Dispose();


        }

    }
}
