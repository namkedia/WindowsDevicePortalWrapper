﻿//----------------------------------------------------------------------------------------------
// <copyright file="RemoteSettings.cs" company="Microsoft Corporation">
//     Licensed under the MIT License. See LICENSE.TXT in the project root license information.
// </copyright>
//----------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Tools.WindowsDevicePortal
{

    public partial class DevicePortal
    {
        public static readonly string RemoteSettingsStatusApi = "api/iot/remote/status";

        /// <summary>
        /// Gets the Remote Settings Status Information.
        /// </summary>
        /// <returns>String containing the Remote Settings Status information.</returns>
        public async Task<RemoteSettingsStatusInfo> GetRemoteSettingsStatusInfo()
        {
            return await this.Get<RemoteSettingsStatusInfo>(RemoteSettingsStatusApi);
        }

        #region Data contract

        /// <summary>
        /// Remote Settings Status information.
        /// </summary>
        [DataContract]
        public class RemoteSettingsStatusInfo
        {
            /// <summary>
            /// Returns true if the service is running
            /// </summary>
            [DataMember(Name = "IsRunning")]
            public bool IsRunning;

            /// <summary>
            ///  Returns true if the service is scheduled.
            /// </summary>
            [DataMember(Name = "IsScheduled")]
            public bool IsScheduled;
        }
        #endregion // Data contract
    }
}
