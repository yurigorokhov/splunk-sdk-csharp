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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Splunk.ModularInputs
{
    /// <summary>
    ///     Represents a parameter that contains a multivalue.
    /// </summary>
    [XmlRoot("param_list")]
    public class MultiValueParameter : ParameterBase
    {
        /// <summary>
        ///     Gets or sets the value of the parameter
        /// </summary>
        [XmlElement("value")]
        public MultiValue ValueXmlElements { get; set; }

        /// <summary>
        ///     Gets the value of the parameter
        /// </summary>
        internal override ValueBase ValueAsBaseType
        {
            get { return ValueXmlElements; }
        }

        /// <summary>
        /// Represents a multivalue.
        /// </summary>
        [SuppressMessage(
            "Microsoft.StyleCop.CSharp.DocumentationRules",
            "SA1600:ElementsMustBeDocumented",
            Justification =
                "Internal class. Pure passthrough")]
        public class MultiValue : ValueBase, IList<string>
        {
            private readonly List<string> value = new List<string>();

            public int Count
            {
                get { return value.Count; }
            }

            public bool IsReadOnly
            {
                get { return ((ICollection<string>)value).IsReadOnly; }
            }
            
            public string this[int index]
            {
                get { return value[index]; }
                set { this.value[index] = value; }
            }
            
            public int IndexOf(string item)
            {
                return value.IndexOf(item);
            }

            public void Insert(int index, string item)
            {
                value.Insert(index, item);
            }

            public void RemoveAt(int index)
            {
                value.RemoveAt(index);
            }

            public void Add(string item)
            {
                value.Add(item);
            }

            public void Clear()
            {
                value.Clear();
            }

            public bool Contains(string item)
            {
                return value.Contains(item);
            }

            public void CopyTo(string[] array, int arrayIndex)
            {
                value.CopyTo(array, arrayIndex);
            }

            public bool Remove(string item)
            {
                return value.Remove(item);
            }

            public IEnumerator<string> GetEnumerator()
            {
                return value.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        /// Used for serialize and deseriallize the value XML element 
        /// for a multivalue parameter.
        /// </summary>
        [XmlRoot("value")]
        public class ValueXmlElement
        {
            /// <summary>
            ///     Gets or sets the value of the parameter
            /// </summary>
            [XmlText]
            public string Text { get; set; }

            /// <summary>
            ///     Returns the string value
            /// </summary>
            /// <returns>
            ///     The string value
            /// </returns>
            public override string ToString()
            {
                return Text;
            }

            /// <summary>
            ///     Convert to a <c>string</c>.
            ///     Same as <see cref="ToString" />
            /// </summary>
            /// <param name="value">Field value</param>
            /// <returns>
            ///     The string value
            ///     <see cref="DefaultDelimiter" />.
            /// </returns>
            public static implicit operator string(ValueXmlElement value)
            {
                return value.ToString();
            }
        }
    }
}