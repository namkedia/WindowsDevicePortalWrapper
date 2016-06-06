﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE.TXT in the project root license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Tools.WindowsDevicePortal
{
    public partial class DevicePortal
    {
        private static readonly String _installedPackagesApi = "api/app/packagemanager/packages";
        private static readonly String _installStateApi = "api/app/packagemanager/state";
        private static readonly String _packageManagerApi = "api/app/packagemanager/package";

        public ApplicationInstallStatusEventHandler AppInstallStatus;

        /// <summary>
        /// Gets the collection of applications installed on the device.
        /// </summary>
        /// <returns>AppPackages object containing the list of installed application packages.</returns>
        public async Task<AppPackages> GetInstalledAppPackages()
        {
            return await Get<AppPackages>(_installedPackagesApi);
        }

        public async Task InstallApplication(/* BUGBUG */)
        {
            // BUGBUG
            throw new NotImplementedException();
        }

        /// <summary>
        /// Uninstalls the specified application.
        /// </summary>
        /// <param name="packageName">The name of the application package to uninstall.</param>
        public async Task UninstallApplication(String packageName)
        {
            
            await Delete(_packageManagerApi,
                                // NOTE: When uninstalling an app package, the package name is not Hex64 encoded.
                                String.Format("package={0}", packageName));
        }

        private void SendAppInstallStatus(AppInstallStatus status,
                                        AppInstallPhase phase,
                                        String message = "")
        {
            AppInstallStatus?.Invoke(this,
                                    new ApplicationInstallStatusEventArgs(status, phase, message));
        }
    }

#region Data contract
    [DataContract]
    public class AppPackages
    {
        [DataMember(Name="InstalledPackages")]
        public List<PackageInfo> Packages { get; set; }
    }

    [DataContract]
    public class PackageInfo
    {
        [DataMember(Name="Name")]
        public String Name { get; set; }

        [DataMember(Name="PackageFamilyName")]
        public String FamilyName { get; set; }

        [DataMember(Name="PackageFullName")]
        public String FullName { get; set; }

        [DataMember(Name="PackageRelativeId")]
        public String AppId { get; set; }

        [DataMember(Name="Publisher")]
        public String Publisher { get; set; }

        [DataMember(Name="Version")]
        public PackageVersion Version { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, Version);
        }
    }

    [DataContract]
    public class PackageVersion
    {
        [DataMember(Name="Build")]
        public Int32 Build { get; set; }

        [DataMember(Name="Major")]
        public Int32 Major { get; set; }

        [DataMember(Name="Minor")]
        public Int32 Minor { get; set; }

        [DataMember(Name="Revision")]
        public Int32 Revision { get; set; }

        public Version Version
        {
            get { return new Version(Major, Minor, Build, Revision); }
        }

        public override string ToString()
        {
            return Version.ToString();
        }
    }
#endregion // Data contract
}