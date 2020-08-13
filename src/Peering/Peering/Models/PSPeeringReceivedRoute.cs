// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.PowerShell.Cmdlets.Peering.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The properties that define a received route.
    /// </summary>
    public partial class PSPeeringReceivedRoute
    {
        /// <summary>
        /// Initializes a new instance of the PSPeeringReceivedRoute class.
        /// </summary>
        public PSPeeringReceivedRoute()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PSPeeringReceivedRoute class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="nextHop">The next hop for the prefix.</param>
        /// <param name="asPath">The AS path for the prefix.</param>
        /// <param name="originAsValidationState">The origin AS change
        /// information for the prefix.</param>
        /// <param name="rpkiValidationState">The RPKI validation state for the
        /// prefix and origin AS that's listed in the AS path.</param>
        /// <param name="trustAnchor">The authority which holds the Route
        /// Origin Authorization record for the prefix, if any.</param>
        /// <param name="receivedTimestamp">The received timestamp associated
        /// with the prefix.</param>
        public PSPeeringReceivedRoute(string prefix = default(string), string nextHop = default(string), string asPath = default(string), string originAsValidationState = default(string), string rpkiValidationState = default(string), string trustAnchor = default(string), string receivedTimestamp = default(string))
        {
            Prefix = prefix;
            NextHop = nextHop;
            AsPath = asPath;
            OriginAsValidationState = originAsValidationState;
            RpkiValidationState = rpkiValidationState;
            TrustAnchor = trustAnchor;
            ReceivedTimestamp = receivedTimestamp;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        [JsonProperty(PropertyName = "prefix")]
        public string Prefix { get; private set; }

        /// <summary>
        /// Gets the next hop for the prefix.
        /// </summary>
        [JsonProperty(PropertyName = "nextHop")]
        public string NextHop { get; private set; }

        /// <summary>
        /// Gets the AS path for the prefix.
        /// </summary>
        [JsonProperty(PropertyName = "asPath")]
        public string AsPath { get; private set; }

        /// <summary>
        /// Gets the origin AS change information for the prefix.
        /// </summary>
        [JsonProperty(PropertyName = "originAsValidationState")]
        public string OriginAsValidationState { get; private set; }

        /// <summary>
        /// Gets the RPKI validation state for the prefix and origin AS that's
        /// listed in the AS path.
        /// </summary>
        [JsonProperty(PropertyName = "rpkiValidationState")]
        public string RpkiValidationState { get; private set; }

        /// <summary>
        /// Gets the authority which holds the Route Origin Authorization
        /// record for the prefix, if any.
        /// </summary>
        [JsonProperty(PropertyName = "trustAnchor")]
        public string TrustAnchor { get; private set; }

        /// <summary>
        /// Gets the received timestamp associated with the prefix.
        /// </summary>
        [JsonProperty(PropertyName = "receivedTimestamp")]
        public string ReceivedTimestamp { get; private set; }

    }
}
