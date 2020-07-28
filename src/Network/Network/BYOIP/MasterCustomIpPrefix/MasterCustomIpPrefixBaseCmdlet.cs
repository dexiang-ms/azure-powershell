﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.Network
{
    using Microsoft.Azure.Commands.Network.Models;
    using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
    using Microsoft.Azure.Management.Network;
    using Microsoft.Azure.Management.Network.Models;
    using System.Net;

    public abstract class MasterCustomIpPrefixBaseCmdlet : NetworkBaseCmdlet
    {
        public IMasterCustomIPPrefixesOperations MasterCustomIpPrefixClient
        {
            get
            {
                return NetworkClient.NetworkManagementClient.MasterCustomIPPrefixes;
            }
        }

        public PSMasterCustomIpPrefix GetMasterCustomIpPrefix(string resourceGroupName, string name, string expandResource = null)
        {
            var masterCustomIpPrefix = this.MasterCustomIpPrefixClient.Get(resourceGroupName, name, expandResource);

            var psMasterCustomIpPrefix = ToPsMasterCustomIpPrefix(masterCustomIpPrefix);
            psMasterCustomIpPrefix.ResourceGroupName = resourceGroupName;

            return psMasterCustomIpPrefix;
        }

        public PSMasterCustomIpPrefix ToPsMasterCustomIpPrefix(MasterCustomIpPrefix publicIpPrefix)
        {
            var psMasterCustomIpPrefix = NetworkResourceManagerProfile.Mapper.Map<PSMasterCustomIpPrefix>(publicIpPrefix);

            psMasterCustomIpPrefix.Tag = TagsConversionHelper.CreateTagHashtable(publicIpPrefix.Tags);

            return psMasterCustomIpPrefix;
        }
    }
}
