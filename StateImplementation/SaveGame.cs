using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bomberman.StateInterfaces;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

namespace Bomberman.StateImplementation
{
    public class SaveGame: ISaveGameState
    {
        private readonly GameSession session;
        private  StorageDevice device;
        string containerName = "MyGamesStorage";
        string filename = "mysave.sav";

        public SaveGame(GameSession session)
        {
            this.session = session;

        }

        public void Save()
        {
            if (!Guide.IsVisible)
            {
                device = null;
                StorageDevice.BeginShowSelector(PlayerIndex.One, SaveToDevice, null);
            }
        }

        void SaveToDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {

                IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
                result.AsyncWaitHandle.WaitOne();
                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists(filename))
                    container.DeleteFile(filename);
                Stream stream = container.CreateFile(filename);
                XmlSerializer serializer = new XmlSerializer(typeof(GameSession));
                serializer.Serialize(stream, session);
                stream.Close();
                container.Dispose();
                result.AsyncWaitHandle.Close();
            }
        }

    }
}
