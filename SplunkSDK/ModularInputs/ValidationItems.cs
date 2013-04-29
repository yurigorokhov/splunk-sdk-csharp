﻿/*
 * Copyright 2013 Splunk, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"): you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System.IO;
using System.Xml.Serialization;

namespace Splunk.ModularInputs
{
    /// <summary>
    /// Input configuration for validation.
    /// </summary>
    [XmlRoot("items")]
    public class ValidationItems : InputConfigurationBase
    {
        /// <summary>
        /// The item that represents an instance of this modular input.
        /// </summary>
        [XmlElement("item")]
        public ConfigurationItem Item { get; set; }

        /// <summary>
        /// Reads the input stream specified and returns the parsed XML input.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>An InputDefinition object.</returns>
        public static ValidationItems Read(TextReader input)
        {
            var x = new XmlSerializer(typeof(ValidationItems));
            var id = (ValidationItems) x.Deserialize(input);
            return id;
        }

        /// <summary>
        /// Serializes this object to XML output. 
        /// </summary>
        /// <returns>The XML String.</returns>
        /// <remarks>This method is used by unit tests.</remarks>
        internal string Serialize()
        {
            var x = new XmlSerializer(typeof(ValidationItems));
            var sw = new StringWriter();
            x.Serialize(sw, this);
            return sw.ToString();
        }
    }
}