using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>Struct representing a message claiming to have been signed with the given signature.</summary>
    public struct SignatureClaim
    {
        /// <summary>Message.</summary>
        [Required]
        public string Message { get; set; }
        /// <summary>Signature in a base64 format, as given by /sign.</summary>
        [Required]
        public string Signature { get; set; }

        /// <summary>Constructor.</summary>
        public SignatureClaim(string message, string signature)
        {
            Message = message;
            Signature = signature;
        }
    }
}