using System;
using System.Linq;

namespace salary.dal.repository
{
    public static class GuidExtension
    {
        public static byte[] ToMySqlByteOrder(this Guid guid)
        {
            var bytes = guid.ToByteArray();
            var span = bytes.AsSpan();
            
            span.Slice(0, 4).Reverse(); // mysql and .net byte[] array has different order 
            span.Slice(4, 2).Reverse();// mysql and .net byte[] array has different order
            span.Slice(6, 2).Reverse();// mysql and .net byte[] array has different order
            
            return bytes;
        }
        
        public static Guid CreateGuidFromMySqlByteOrder(this byte[] inbytes)
        {
            var bytes = inbytes.ToArray();//deep copy
            var bytesSpan = bytes.AsSpan();
            bytesSpan.Slice(0, 4).Reverse(); // mysql and .net byte[] array has different order 
            bytesSpan.Slice(4, 2).Reverse();// mysql and .net byte[] array has different order
            bytesSpan.Slice(6, 2).Reverse();// mysql and .net byte[] array has different order
            
            return new Guid(bytes);
        }
    }
}