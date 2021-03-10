using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kritzel.WebCast
{
    public class Cast
    {
        const long LIFETIME = 30000;

        public int version = -1;
        public long lifetime;
        public string id;
        public byte[] data = null;

        public Cast(long currentTime, string id)
        {
            lifetime = currentTime + LIFETIME * 2;
            this.id = id;
        }

        public void Update(long currentTime)
        {
            lifetime = currentTime + LIFETIME * 2;
        }

        public byte[] GetData()
        {
            return data;
        }

        public void SetData(byte[] data)
        {
            this.data = data;
        }

        public void Destroy()
        {
            
        }
    }
}
