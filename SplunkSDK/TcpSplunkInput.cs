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

namespace Splunk
{
    /// <summary>
    /// The <see cref="TcpSplunkInput"/> class represents a Splunk-processed
    /// "cooked" TCP data input. This differs from a raw TCP input in that this 
    /// cooked TCP data is processed by Splunk and is not in raw form.
    /// </summary>
    public class TcpSplunkInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpSplunkInput"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="path">The path.</param>
        public TcpSplunkInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the host of the remote server that sends data. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's possible values are:
        /// <list type="bullet">
        /// <item><b>"ip"</b> sets the host to the IP address of the remote 
        /// server sending data.</item>
        /// <item><b>"dns"</b> sets the host to the reverse DNS entry for the
        /// IP address of the remote server sending data.</item>
        /// <item><b>"none"</b> leaves the host as specified in inputs.conf,
        /// which is typically the Splunk system hostname.</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property's default value is "ip".
        /// </para>
        /// </remarks>
        public string ConnectionHost
        {
            get
            {
                return this.GetString("connection_host", null);
            }

            set
            {
                this.SetCacheValue("connection_host", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether this input is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Gets the group of this TCP input.
        /// </summary>
        public string Group
        {
            get
            {
                return this.GetString("group");
            }
        }

        /// <summary>
        /// Gets or sets the source host of this TCP input where this indexer 
        /// gets its data.
        /// </summary>
        public string Host
        {
            get
            {
                return this.GetString("host", null);
            }

            set
            {
                this.SetCacheValue("host", value);
            }
        }

        /// <summary>
        /// Gets or sets the index name of this TCP input.
        /// </summary>
        public string Index
        {
            get
            {
                return this.GetString("index", null);
            }

            set
            {
                this.SetCacheValue("index", value);
            }
        }

        /// <summary>
        /// Gets the input type of this object, Tcp (cooked).
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.TcpSplunk;
            }
        }

        /// <summary>
        /// Gets or sets the queue for this TCP input. 
        /// </summary>
        /// <remarks>
        /// This property's valid values are:
        /// <list type="bullet">
        /// <item>"parsingQueue"</item> 
        /// <item>"indexQueue"</item> 
        /// </list>
        /// </remarks>
        public string Queue
        {
            get
            {
                return this.GetString("queue", null);
            }

            set
            {
                this.SetCacheValue("queue", value);
            }
        }

        /// <summary>
        /// Gets the incoming host restriction for this TCP input. When 
        /// specified, this input only accepts data from the specified host. 
        /// </summary>
        public string RestrictToHost
        {
            get
            {
                return this.GetString("restrictToHost", null);
            }
        }

        /// <summary>
        /// Gets the initial source key for this TCP input. Typically 
        /// this value is the input file path.
        /// </summary>
        public string Source
        {
            get
            {
                return this.GetString("source", null);
            }

            set
            {
                this.SetCacheValue("source", value);
            }
        }

        /// <summary>
        /// Gets or sets the source type for events from this TCP input.
        /// </summary>
        public string SourceType
        {
            get
            {
                return this.GetString("sourcetype", null);
            }

            set
            {
                this.SetCacheValue("sourcetype", value);
            }                
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether this TCP input is using 
        /// secure socket layer (SSL).
        /// </summary>
        public bool SSL
        {
            get
            {
                return this.GetBoolean("SSL", false);
            }

            set
            {
                this.SetCacheValue("SSL", value);
            }
        }

        /// <summary>
        /// Returns an object containing the inbound raw TCP connections.
        /// </summary>
        /// <returns>The connections.</returns>
        public TcpConnections Connections()
        {
            return new TcpConnections(this.Service, this.Path + "/connections");
        }
    }
}
