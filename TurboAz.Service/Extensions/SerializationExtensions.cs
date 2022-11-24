using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Service.Infrastructure;

namespace TurboAz.Service.Extensions
{

    public static class SerializationExtensions
    {
        public static byte[] ToByteArray(this object objectToSerialize)
        {
            if (objectToSerialize == null)
            {
                return null;
            }

            return Encoding.Default.GetBytes(JsonConvert.SerializeObject(objectToSerialize));
        }

        public static T FromByteArray<T>(this byte[] arrayToDeserialize) where T : class
        {
            if (arrayToDeserialize is null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(Encoding.Default.GetString(arrayToDeserialize));
        }

        public static Endpoint? GetEndpoint(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Features.Get<IEndpointFeature>()?.Endpoint;
        }

        /// <summary>
        /// Extension method for setting the <see cref="Endpoint"/> for the current request.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> context.</param>
        /// <param name="endpoint">The <see cref="Endpoint"/>.</param>
        public static void SetEndpoint(this HttpContext context, Endpoint? endpoint)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var feature = context.Features.Get<IEndpointFeature>();

            if (endpoint != null)
            {
                if (feature == null)
                {
                    feature = new EndpointFeature();
                    context.Features.Set(feature);
                }

                feature.Endpoint = endpoint;
            }
            else
            {
                if (feature == null)
                {
                    // No endpoint to set and no feature on context. Do nothing
                    return;
                }

                feature.Endpoint = null;
            }
        }

        private class EndpointFeature : IEndpointFeature
        {
            public Endpoint? Endpoint { get; set; }
        }
    }

}
