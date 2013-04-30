﻿/*
 * Copyright 2012 Splunk, Inc.
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
    ///     Contains arguments for getting job results
    ///     using the <seealso cref="Job" /> class.
    /// </summary>
    public class JobResultsArgs : Args
    {
        /// <summary>
        ///     Specifies the format for the returned output.
        /// </summary>
        // C# disallow nested class to have the same name as
        // a property. Use 'Enum' suffix to differentiate.
        public enum OutputModeEnum
        {
            /// <summary>
            ///     Returns output in Atom format.
            /// </summary>
            [CustomString("atom")]
            Atom,

            /// <summary>
            ///     Returns output in CSV format.
            /// </summary>
            [CustomString("csv")]
            Csv,

            /// <summary>
            ///     Returns output in JSON format.
            /// </summary>
            [CustomString("json")]
            Json,

            /// <summary>
            ///     Returns output in JSON_COLS format.
            /// </summary>
            [CustomString("json_cols")]
            JsonColumns,

            /// <summary>
            ///     Returns output in JSON_ROWS format.
            /// </summary>
            [CustomString("json_rows")]
            JsonRows,

            /// <summary>
            ///     Returns output in raw format.
            /// </summary>
            [CustomString("raw")]
            Raw,

            /// <summary>
            ///     Returns output in XML format.
            /// </summary>
            [CustomString("xml")]
            Xml,
        }

        /// <summary>
        ///     Sets the maximum number of results to return. To return all available results, specify 0.
        /// </summary>
        public new int Count
        {
            set { this["count"] = value; }
        }

        /// <summary>
        ///     Sets a list of fields to return for the event set.
        /// </summary>
        public virtual string[] FieldList
        {
            set { this["f"] = value; }
        }

        /// <summary>
        ///     Specifies the index of the first result (inclusive)
        ///     from which to begin returning data.
        ///     This value is 0-indexed.
        /// </summary>
        public virtual int Offset
        {
            set { this["offset"] = value; }
        }

        /// <summary>
        ///     Sets the format of the output.
        /// </summary>
        public virtual OutputModeEnum OutputMode
        {
            set { this["output_mode"] = value.GetCustomString(); }
        }

        /// <summary>
        ///     Sets the post-processing search to apply to results.
        /// </summary>
        public virtual string Search
        {
            set { this["search"] = value; }
        }
    }
}