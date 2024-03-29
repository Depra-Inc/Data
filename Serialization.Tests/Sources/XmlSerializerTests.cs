﻿using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Xml.Impl;

namespace Depra.Data.Serialization.Tests.Sources
{
    internal class XmlSerializerTests : SerializerTestsBase
    {
        protected override ISerializer CreateSerializer()
        {
            var adapter = new StandardXmlSerializerAdapter();
            var serializer = new XmlSerializer(adapter);

            return serializer;
        }
    }
}