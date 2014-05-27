using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml.Serialization;
using Bomberman.StateInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

namespace Bomberman.StateImplementation
{
    public class LoadGame : ILoadGameState
    {
        private readonly GameSession session;
        private StorageDevice device;
        private const string containerName = "MyGamesStorage";
        private const string filename = "mysave.sav";
        private StorageContainer container;

        public object LoadGameState<T>()
        {
            if (!Guide.IsVisible)
            {
                device = null;
                var result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                result.AsyncWaitHandle.WaitOne();
                LoadFromDevice(result);
                result.AsyncWaitHandle.Close();
                return (T)Serialize<T>(container);
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

        private static object Serialize<T>(StorageContainer container)
        {
            if (container.FileExists(filename))
            {
                Stream stream = container.OpenFile(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var result = (T)serializer.Deserialize(stream);
                stream.Close();
                container.Dispose();
                return result;
            }
            return null;
        }
    }
}
