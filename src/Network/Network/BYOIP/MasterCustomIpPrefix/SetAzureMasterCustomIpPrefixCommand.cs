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
    using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
    using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
    using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
    using Microsoft.Azure.Management.Network;
    using Microsoft.WindowsAzure.Commands.Utilities.Common;
    using System;
    using System.Collections;
    using System.Management.Automation;
    using MNM = Microsoft.Azure.Management.Network.Models;

    [Cmdlet(VerbsCommon.Set, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "MasterCustomIpPrefix", SupportsShouldProcess = true), OutputType(typeof(PSMasterCustomIpPrefix))]
    public class SetAzureMasterCustomIpPrefixCommand : MasterCustomIpPrefixBaseCmdlet
    {
        private const string SetByNameParameterSet = "SetByNameParameterSet";
        private const string SetByInputObjectParameterSet = "SetByInputObjectParameterSet";
        private const string SetByResourceIdParameterSet = "SetByResourceIdParameterSet";

        [Parameter(
           Mandatory = false,
           ValueFromPipelineByPropertyName = true,
           HelpMessage = "The resource name.",
           ParameterSetName = SetByNameParameterSet)]
        [ResourceNameCompleter("Microsoft.Network/masterCustomIpPrefix", "ResourceGroupName")]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public virtual string Name { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The resource group name.",
            ParameterSetName = SetByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public virtual string ResourceGroupName { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipeline = true,
            HelpMessage = "The MasterCustomIpPrefix to set.",
            ParameterSetName = SetByInputObjectParameterSet)]
        public PSMasterCustomIpPrefix InputObject { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The resource Id.",
            ParameterSetName = SetByResourceIdParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public virtual string ResourceId { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The MasterCustomIpPrefix location.",
            ParameterSetName = SetByResourceIdParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = SetByInputObjectParameterSet)]
        [LocationCompleter("Microsoft.Network/masterCustomIpPrefix")]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        [Parameter(
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "A hashtable which represents resource tags.",
            ParameterSetName = SetByNameParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = SetByResourceIdParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = SetByInputObjectParameterSet)]
        public Hashtable Tag { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        public override void Execute()
        {
            base.Execute();

            if (this.IsParameterBound(c => c.ResourceId))
            {
                var resourceInfo = new ResourceIdentifier(ResourceId);
                this.ResourceGroupName = resourceInfo.ResourceGroupName;
                this.Name = resourceInfo.ResourceName;
            }
            else if (this.IsParameterBound(c => c.InputObject))
            {
                this.ResourceGroupName = InputObject.ResourceGroupName;
                this.Name = InputObject.Name;
                this.Location = this.IsParameterBound(c => c.Location) ? Location : InputObject.Location;
            }

            if (!NetworkBaseCmdlet.IsResourcePresent(() => GetMasterCustomIpPrefix(this.ResourceGroupName, this.Name)))
            {
                throw new ArgumentException(Microsoft.Azure.Commands.Network.Properties.Resources.ResourceNotFound);
            }


            var psModel = new PSMasterCustomIpPrefix()
            {
                Name = InputObject.Name,
                ResourceGroupName = InputObject.ResourceGroupName,
                Location = this.Location,
            };
            var sdkModel = NetworkResourceManagerProfile.Mapper.Map<MNM.MasterCustomIpPrefix>(psModel);

            if (this.IsParameterBound(c => c.InputObject))
            {
                sdkModel.Tags = TagsConversionHelper.CreateTagDictionary(this.IsParameterBound(c => c.Tag) ? this.Tag : InputObject.Tag, validate: true);
            }
            else
            {
                sdkModel.Tags = TagsConversionHelper.CreateTagDictionary(this.Tag, validate: true);
            }

            if (this.ShouldProcess($"Name: {this.Name} ResourceGroup: {this.ResourceGroupName}", "Set existing MasterCustomIpPrefix"))
            {
                // Execute the PUT MasterCustomIpPrefix Policy call
                var modifiedSdkModel = this.MasterCustomIpPrefixClient.CreateOrUpdate(this.ResourceGroupName, this.Name, sdkModel);
                var modifiedPsModel = this.ToPsMasterCustomIpPrefix(modifiedSdkModel);
                modifiedPsModel.ResourceGroupName = NetworkBaseCmdlet.GetResourceGroup(modifiedPsModel.Id);
                WriteObject(modifiedPsModel);
            }
        }
    }
}
